using System;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using System.Configuration;
using System.Net.Mail;
using System.DirectoryServices.AccountManagement;
using System.Threading;
using System.Data.SqlClient;

namespace ServerAlertManager
{
    public partial class FormRh : Form
    {
        Thread th;
        private DateTime timer;
        private int hr;
        private int seg;
        private int min;
        string connectionStrings = ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString;
        public FormRh()
        {
            InitializeComponent();
            LblPc.Visible = true;
            LblUser.Visible = true;
            lblMail.Visible = true;
            lLblWdid.Visible = true;
        }
        private void FormWorkday_Load(object sender, EventArgs e)
        {

            this.Hide();
            PBNo.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            LblTradeMark.Text = "© " + DateTime.Now.ToString("yyyy") + " BEL IT APPLICATIONS ";
            Get_User();

        }
        void Get_User()
        {

            PrincipalContext ActiveDirectoryUser = new PrincipalContext(ContextType.Domain, "corp.jabil.org");
            UserPrincipal userName = new UserPrincipal(ActiveDirectoryUser);
            userName.SamAccountName = Environment.UserName;
            PrincipalSearcher search = new PrincipalSearcher(userName);
            foreach (UserPrincipal result in search.FindAll())
                if (result.DisplayName != null)
                {
                    label7.Text = (result.DisplayName);
                    lblMail.Text = (result.EmailAddress);
                    lLblWdid.Text = (result.EmployeeId);
                }
            search.Dispose();
            CheckGetUser();
        }
        private void Counter()
        {
            this.Show();
            WindowState = FormWindowState.Maximized;
            Audio_Start();
            LblPc.Text = Environment.MachineName;
            LblUser.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            try
            {
                timer = Convert.ToDateTime(textBox1.Text);
                hr = timer.Hour;
                min = timer.Minute;
                seg = timer.Second;
                timer1.Start();
                textBox1.Enabled = false;
                progressBar1.Step = 1;
                progressBar1.Maximum = (min * 60) + seg + (hr * 60 * 60);
                progressBar1.Minimum = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CheckGetUser()
        {
            string searchString = UserPrincipal.Current.EmployeeId;
            bool foundText = false;

            using (SqlConnection connection = new SqlConnection(connectionStrings))
            {
                connection.Open();
                foundText = CheckEmployeeNotification(connection, searchString);

                if (foundText)
                {
                    string message = GetNotificationMessage(connection);
                    if (!string.IsNullOrEmpty(message))
                    {
                        textBox2.Text = message;
                    }
                    Counter();
                }
                else
                {
                    this.Close();
                    StartNewFormThread();
                }
            }
        }

        private bool CheckEmployeeNotification(SqlConnection connection, string searchString)
        {
            string query = "SELECT COUNT(*) FROM [AlertManager].[dbo].[Rh] WHERE EmployeeID = @searchString AND Notification = 1";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@searchString", searchString);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private string GetNotificationMessage(SqlConnection connection)
        {
            string query = "SELECT TOP (1) [Message] FROM [AlertManager].[dbo].[Notification] WHERE Area = 'RhTraining'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return command.ExecuteScalar()?.ToString();
            }
        }

        private void StartNewFormThread()
        {
            Thread th = new Thread(OpenNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OpenNewForm(object obj)
        {
           
            Application.Run(new FormLpaVls());

        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = (min).ToString("00") + ":" + (seg).ToString("00");
            progressBar1.Increment(progressBar1.Step);
            if (seg == 0 && min > 0)
            {
                min--;
                seg = 59;
            }

            if (min == 00 && seg == 00)
            {
                label1.Text = (min).ToString("00") + ":" + (seg).ToString("00");
                timer1.Stop();
                Audio_End();
                button1.Visible = true;
                button2.Visible = true;
                progressBar1.Visible = false;
            }
            seg--;
        }
        void Audio_End()
        {
            SoundPlayer sndplayr = new
            SoundPlayer(Properties.Resources.Timeisup);
            sndplayr.Play();
        }
        void Audio_Start()
        {
            SoundPlayer sndplayr = new
            SoundPlayer(Properties.Resources.Windows_Logon);
            sndplayr.Play();
        }
        void Audio_Error()
        {
            SoundPlayer sndplayr = new
            SoundPlayer(Properties.Resources.Nice);
            sndplayr.Play();
        }
        void Done()
        {

            Process.Start("https://wd5.myworkday.com/jabil/d/task/2997$14735.htmld?metadataEntryPoint=%2Fjabil%2Flearning%2Fmylearning");
            this.Close();
            th = new Thread(OpenNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void Msg_install()
        {
            string Logo = "http://brbelm0apps02/5simages/workDay.png";
            var year = DateTime.Now.Year;
            string Themessage = @"<html>
                                <body>
                                <table width=""100%"">
                                <tr>
                                <h1 style=""text-align:center"">Pendências no Processo de Desenvolvimento</h1>
                                <p style=""text-align:center"">Olá  Administrator,</p>
                                <p style=""text-align:center"">O Colaborador: " + label7.Text + @"  WORDAY ID: " + lLblWdid.Text + @".</p>
                                <p style=""text-align:center"">reportou o término de seus treinamentos em: " + DateTime.Now + @", geltileza verificar.</p>
                                <p style=""text-align:center""><img alt=""Logo"" src=" + Logo + @" style=""height:100px; width:100px"" /><br />
                                &nbsp;</p><p style=""text-align:center""><strong>Automatic notification do not respond.</strong></p>
                                <p style=""text-align:center"">&copy; JABIL NOTIFY  " + year + @"</p>
                                </tr>
                                </table>
                                </body>
                                </html>";

            sendHtmlEmail("no-reply@jabil.com", "jean_menezes@jabil.com", lblMail.Text, Themessage, "WORDAY NOTIFY", "WARNING", "corimc04", 25);
        }
        private void sendHtmlEmail(string from_Email, string to_Email, string cc_Email, string body, string from_Name, string Subject, string SMTP_IP, Int32 SMTP_Server_Port)
        {
            //create an instance of new mail message
            MailMessage mail = new MailMessage();

            //set the HTML format to true
            mail.IsBodyHtml = true;

            //create Alrternative HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

            //Add view to the Email Message
            mail.AlternateViews.Add(htmlView);

            //set the "from email" address and specify a friendly 'from' name
            mail.From = new MailAddress(from_Email, from_Name);

            //set the "to" email address
            mail.To.Add(to_Email);

            //set the "cc" email address
            mail.CC.Add(cc_Email);

            //set the Email subject
            mail.Subject = Subject;

            //set the SMTP info
            SmtpClient smtp = new SmtpClient(SMTP_IP, SMTP_Server_Port);

            //send the email
            smtp.Send(mail);
            // updateProgress("SEND EMAIL..");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Done();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Msg_install();
            Thread.Sleep(TimeSpan.FromMinutes(1));
            this.Close();
            th = new Thread(OpenNewForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();


        }
        private void form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ModifierKeys == Keys.Alt || ModifierKeys == Keys.F4)
            {
                e.Cancel = true;
                PBNo.Visible = true;
                Audio_Error();
                
                AutoClosingMessageBox.Show("Nice try, but I don't think so....", "Jabil Alert Manager", 2000);
                PBNo.Visible = false;
            }

        }
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;

            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);

                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);

            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();

            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

    }
}
