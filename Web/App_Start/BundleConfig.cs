using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new StyleBundle("~/Resources/styles")
                .Include("~/Resources/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Resources/ionicons/css/ionicons.css", new CssRewriteUrlTransform())
                .Include("~/Resources/plugins/daterangepicker/daterangepicker.css")
                .Include("~/Resources/plugins/fullcalendar/lib/cupertino/jquery-ui.min.css")
                .Include("~/Resources/plugins/fullcalendar/fullcalendar.css")
                .Include("~/Resources/bootstrap/css/bootstrap.css")
                .Include("~/Resources/plugins/select2/css/select2.css")
                .Include("~/Resources/plugins/select2/css/select2-bootstrap.css")
                .Include("~/Resources/dist/css/AdminLTE.css")
                .Include("~/Resources/css/site.css")
                .Include("~/Resources/plugins/datatables/dataTables.bootstrap.css")
                .Include("~/Resources/plugins/datatables/extensions/RowReorder/css/rowReorder.dataTables.min.css")
                .Include("~/Resources/plugins/toastr/toastr.css")
                .Include("~/Resources/plugins/iCheck/all.css")
                .Include("~/Resources/plugins/datepicker/datepicker3.css")
                .Include("~/Resources/plugins/timepicker/bootstrap-timepicker.min.css")
                .Include("~/Resources/plugins/touchspin/jquery.bootstrap-touchspin.min.css")
                .Include("~/Resources/plugins/datatables/extensions/Export/buttons.dataTables.min.css")
                .Include("~/Resources/plugins/bootstraptour/bootstrap-tour.min.css")

                //Skins do Layout
                //Atenção: Para utilizar corretamente, descomentar somente a skin a ser utilizada, e 
                //referenciar a classe no arquivo Views\Shared\_Layout.cshtml no BODY Class
                //.Include("~/Resources/dist/css/skins/skin-black.css")
                //.Include("~/Resources/dist/css/skins/skin-black-light.css")
                //.Include("~/Resources/dist/css/skins/skin-blue.css")
                //.Include("~/Resources/dist/css/skins/skin-blue-light.css")
                //.Include("~/Resources/dist/css/skins/skin-green.css")
                //.Include("~/Resources/dist/css/skins/skin-green-light.css")
                //.Include("~/Resources/dist/css/skins/skin-purple.css")
                //.Include("~/Resources/dist/css/skins/skin-purple-light.css")
                //.Include("~/Resources/dist/css/skins/skin-red.css")
                //.Include("~/Resources/dist/css/skins/skin-red-light.css")
                .Include("~/Resources/dist/css/skins/skin-yellow.css")
                //.Include("~/Resources/dist/css/skins/skin-yellow-light.css")

                );


            bundles.Add(new ScriptBundle("~/resources/js").Include(
                "~/Resources/plugins/jQuery/jQuery-2.1.4.min.js",
                "~/Resources/plugins/jQueryUI/jquery-ui.min.js",
                "~/Resources/plugins/fullcalendar/lib/jQuery-ui.custom.min.js",
                "~/Resources/bootstrap/js/bootstrap.min.js",
                //"~/Resources/plugins/tinymce/tinymce.min.js",
                "~/Resources/scripts/bootbox.min.js",
                "~/Resources/dist/js/app.min.js",
                "~/Resources/plugins/jQuery/jquery.unobtrusive*",
                "~/Resources/plugins/jQuery/jquery.validate*",
                "~/Resources/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/Resources/plugins/toastr/toastr.min.js",
                "~/Resources/plugins/datatables/jquery.dataTables.min.js",
                "~/Resources/plugins/datatables/dataTables.bootstrap.min.js",
                "~/Resources/plugins/datatables/extensions/RowReorder/js/dataTables.rowReorder.min.js",
                "~/Resources/plugins/select2/js/select2.full.min.js",
                "~/Resources/plugins/select2/js/i18n/pt-BR.js",
                "~/Resources/plugins/iCheck/icheck.min.js",
                "~/Resources/plugins/fullcalendar/lib/moment.min.js",
                "~/Resources/plugins/datepicker/bootstrap-datepicker.js",
                "~/Resources/plugins/fullcalendar/fullcalendar.min.js",
                "~/Resources/plugins/fullcalendar/lang-all.js",
                "~/Resources/plugins/daterangepicker/daterangepicker.js",
                "~/Resources/plugins/timepicker/bootstrap-timepicker.min.js",
                "~/Resources/plugins/touchspin/jquery.bootstrap-touchspin.min.js",
                "~/Resources/plugins/numeric/jquery.numeric.min.js",
                "~/Resources/plugins/purl/purl.js",
                "~/Resources/scripts/js.cookie.js",
                //"~/Resources/scripts/principal.js",
                "~/Resources/plugins/inputMask/jquery.inputmask.bundle.min.js",
                "~/Resources/plugins/datatables/extensions/Export/buttons.html5.min.js",
                "~/Resources/plugins/datatables/extensions/Export/buttons.print.min.js",
                "~/Resources/plugins/datatables/extensions/Export/dataTables.buttons.min.js",
                "~/Resources/plugins/datatables/extensions/Export/jszip.min.js",
                "~/Resources/plugins/datatables/extensions/Export/pdfmake.min.js",
                "~/Resources/plugins/datatables/extensions/Export/vfs_fonts.js",
                "~/Resources/plugins/datatables/extensions/Moment/datetime-moment.js",
                "~/Resources/plugins/bootstraptour/bootstrap-tour.min.js"

                ));

            bundles.Add(new StyleBundle("~/Resources/stylesPanel")
                .Include("~/Resources/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/Resources/ionicons/css/ionicons.css", new CssRewriteUrlTransform())
                .Include("~/Resources/bootstrap/css/bootstrap.css")
                .Include("~/Resources/dist/css/AdminLTE.css")
                .Include("~/Resources/css/site.css")

                .Include("~/Resources/dist/css/skins/skin-yellow.css")
                );

            bundles.Add(new ScriptBundle("~/resources/jsPanel").Include(
                "~/Resources/plugins/jQuery/jQuery-2.1.4.min.js",
                "~/Resources/plugins/jQueryUI/jquery-ui.min.js",
                "~/Resources/bootstrap/js/bootstrap.min.js",
                "~/Resources/dist/js/app.min.js"
                ));

            bundles.Add(new StyleBundle("~/Resources/stylesAuditing")
                .Include("~/Resources/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Resources/plugins/datatables/dataTables.bootstrap.css")
                .Include("~/Resources/plugins/iCheck/all.css")
                .Include("~/Resources/plugins/select2/css/select2.css")
                .Include("~/Resources/plugins/select2/css/select2-bootstrap.css")
                );

            bundles.Add(new ScriptBundle("~/resources/jsAuditing").Include(
                        "~/Resources/plugins/jQuery/jQuery-2.1.4.min.js",
                        "~/Resources/plugins/jQuery/jquery.unobtrusive*",
                        "~/Resources/plugins/jQuery/jquery.validate*",
                        "~/Resources/bootstrap/js/bootstrap.min.js",
                        "~/Resources/plugins/wizard/jquery.bootstrap.wizard.js",
                        "~/Resources/plugins/datatables/jquery.dataTables.min.js",
                        "~/Resources/plugins/datatables/dataTables.bootstrap.min.js",
                        "~/Resources/plugins/iCheck/icheck.min.js",
                        "~/Resources/scripts/bootbox.min.js",
                        "~/Resources/scripts/principal.js",
                        "~/Resources/plugins/select2/js/select2.full.min.js",
                        "~/Resources/plugins/select2/js/i18n/pt-BR.js"

                        ));
        }
    }
}
