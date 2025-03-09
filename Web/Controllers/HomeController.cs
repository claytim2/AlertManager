using Localization.DataServices;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Data.SqlClient;
using Model.DatabaseContext;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Error(string description = null)
        {
            description = description ?? ResErrors.JMDLoginRequest;

            ViewBag.Title = "Error";
            ViewBag.Description = description;

            return View("_Error");
        }

        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (ConfigurationManager.AppSettings["ApplicationInStage"].ToLower() == "true")
                {
                    return RedirectToAction("ApplicationInStage");
                }
                else
                {
                    ViewBag.Icon = "fa-window-maximize";
                    ViewBag.Title = "Home";
                    ViewBag.SubTitle = "";
                    ViewBag.VideoTutorialUrl = "";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");


            }

        }

        [Authorize]
        public ActionResult ApplicationInStage()
        {
            if (ConfigurationManager.AppSettings["ApplicationInStage"].ToLower() == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult VideoTutorial(string videoSrc)
        {
            ViewBag.VideoTutorialUrl = videoSrc;
            return PartialView();
        }


        public JsonResult GetCount()
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;

            int WorkdayCount = 0;
            int GapAnalysisCount = 0;
            int JosCount = 0;
            int LpaBelCount = 0;
            int LpaVlsCount = 0;
            int RhCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
                                SELECT COUNT([Notification]) AS NotificationCount FROM [AlertManager].[dbo].[Workday] WHERE Notification = 1;
                                SELECT COUNT([Notification]) AS NotificationCount FROM [AlertManager].[dbo].[GapAnalysis] WHERE Notification = 1;
                                SELECT COUNT([Notification]) AS NotificationCount FROM [AlertManager].[dbo].[Jos] WHERE Notification = 1;
                                SELECT COUNT([Notification]) AS NotificationCount FROM [AlertManager].[dbo].[LpaBel] WHERE Notification = 1;
                                SELECT COUNT([Notification]) AS NotificationCount FROM [AlertManager].[dbo].[LpaVls] WHERE Notification = 1;
                                SELECT COUNT([Notification]) AS NotificationCount FROM [AlertManager].[dbo].[Rh] WHERE Notification = 1;  ";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) WorkdayCount = reader.GetInt32(0);
                    if (reader.NextResult() && reader.Read()) GapAnalysisCount = reader.GetInt32(0);
                    if (reader.NextResult() && reader.Read()) JosCount = reader.GetInt32(0);
                    if (reader.NextResult() && reader.Read()) LpaBelCount = reader.GetInt32(0);
                    if (reader.NextResult() && reader.Read()) LpaVlsCount = reader.GetInt32(0);
                    if (reader.NextResult() && reader.Read()) RhCount = reader.GetInt32(0);
                }
            }
            return Json(new { WorkdayCount, GapAnalysisCount, JosCount, LpaBelCount, LpaVlsCount, RhCount }, JsonRequestBehavior.AllowGet);
        }
    }
}