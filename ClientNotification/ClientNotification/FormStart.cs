using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ClientNotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormStart"/> class.
    /// </summary>
    public partial class FormStart : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormStart"/> class.
        /// </summary>
        public FormStart()
        {
            InitializeComponent();
            Thread.Sleep(TimeSpan.FromMinutes(5));
        }

        /// <summary>
        /// Handles the Load event of the FormStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FormStart_Load(object sender, EventArgs e)
        {
            MainConnection();
        }
        /// <summary>
        /// Class to check database connection.
        /// </summary>
        public class DatabaseConnectionChecker
        {
            /// <summary>
            /// Checks the connection to the database.
            /// </summary>
            /// <param name="connectionString">The connection string.</param>
            /// <returns><c>true</c> if connection is successful, <c>false</c> otherwise.</returns>
            public static bool CheckConnection(string connectionString)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        return true; // Conexão bem-sucedida
                    }
                }
                catch (Exception ex)
                {
                    return false; // Falha na conexão
                }
            }
        }

        /// <summary>
        /// Automatically starts the client notification process.
        /// </summary>
        public void AutoStart()
        {
            var clientQuery = "SELECT TOP(1)[Value] FROM[AlertManager].[dbo].[SystemConfiguration] WHERE[Value] LIKE '%.exe%'";
            string connectionString = "Server=BRBELM0QSQL81;Database=AlertManager;User Id=SVC_AlertManager;Password=J@bilIT2025;";
            bool isConnected = DatabaseConnectionChecker.CheckConnection(connectionString);

            if (isConnected)
            {
                string query = "SELECT TOP (1) [Message] FROM [AlertManager].[dbo].[Notification] WHERE Area = 'RHWorkday'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteScalar()?.ToString();
                    }
                }

                try
                {
                    notifyIcon1.ShowBalloonTip(1000, "Jabil Notification Manager", "Verificando treinamentos e ações pendentes.", ToolTipIcon.Info);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(clientQuery, connection))
                        {
                            string client = command.ExecuteScalar()?.ToString();
                            // Removendo a substituição do caminho
                            string correctedPath = client;
                            if (!string.IsNullOrEmpty(correctedPath))
                            {
                                Process exeProcess = Process.Start(correctedPath);
                                Thread.Sleep(TimeSpan.FromMinutes(1));
                                exeProcess?.Close();
                            }
                        }
                    }
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    notifyIcon1.ShowBalloonTip(1000, "Jabil Notification Manager", $"Erro ao iniciar o executável: {ex.Message}", ToolTipIcon.Error);
                    Application.Exit();
                }
            }
            else
            {
                notifyIcon1.ShowBalloonTip(1000, "Jabil Notification Manager", "Sem ações ou treinamentos pendentes.", ToolTipIcon.None);
                Application.Exit();
            }
        }

        /// <summary>
        /// Establishes the main connection and retries if it fails.
        /// </summary>
        public void MainConnection()
        {
            var keepRetrying = true;
            string logonServer = Environment.GetEnvironmentVariable("LOGONSERVER");
            if (!string.IsNullOrEmpty(logonServer) && logonServer.StartsWith("\\\\"))
            {
                logonServer = logonServer.Substring(2); // Remove the initial "\\"
            }
            while (keepRetrying)
            {
                if (PingHost(logonServer))
                {
                    keepRetrying = false;
                    AutoStart();
                }
                else
                {
                    keepRetrying = true;
                    Thread.Sleep(TimeSpan.FromMinutes(5));
                }
            }
        }

        /// <summary>
        /// Pings the specified host to check if it is reachable.
        /// </summary>
        /// <param name="nameOrAddress">The name or address of the host.</param>
        /// <returns><c>true</c> if the host is reachable, <c>false</c> otherwise.</returns>
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }
    }
}

//namespace ClientNotification
//{
//    public partial class FormStart : Form
//    {
//        public FormStart()
//        {
//            InitializeComponent();
//            Thread.Sleep(TimeSpan.FromMinutes(5));
//        }

//        private void FormStart_Load(object sender, EventArgs e)
//        {

//             MainConnection();
//        }

//        public class DatabaseConnectionChecker
//        {
//            public static bool CheckConnection(string connectionString)
//            {
//                try
//                {
//                    using (SqlConnection connection = new SqlConnection(connectionString))
//                    {
//                        connection.Open();
//                        return true; // Conexão bem-sucedida
//                    }
//                }
//                catch (Exception ex)
//                {

//                    return false; // Falha na conexão
//                }
//            }
//        }

//        public void AutoStart()
//        {
//            var clientQuery = "SELECT TOP(1)[Value] FROM[AlertManager].[dbo].[SystemConfiguration] WHERE[Value] LIKE '%.exe%'";
//            string connectionString = "Server=BRBELM0QSQL81;Database=AlertManager;User Id=SVC_AlertManager;Password=J@bilIT2025;";
//            bool isConnected = DatabaseConnectionChecker.CheckConnection(connectionString);

//            if (isConnected)
//            {
//                string query = "SELECT TOP (1) [Message] FROM [AlertManager].[dbo].[Notification] WHERE Area = 'RHWorkday'";
//                using (SqlConnection connection = new SqlConnection(connectionString))
//                {
//                    connection.Open();
//                    using (SqlCommand command = new SqlCommand(query, connection))
//                    {
//                        command.ExecuteScalar()?.ToString();
//                    }
//                }

//                try
//                {
//                    notifyIcon1.ShowBalloonTip(1000, "Jabil Notification Manager", "Verificando treinamentos e ações pendentes.", ToolTipIcon.Info);
//                    using (SqlConnection connection = new SqlConnection(connectionString))
//                    {
//                        connection.Open();
//                        using (SqlCommand command = new SqlCommand(clientQuery, connection))
//                        {
//                            string client = command.ExecuteScalar()?.ToString();
//                            // Removendo a substituição do caminho
//                            string correctedPath = client;
//                            if (!string.IsNullOrEmpty(correctedPath))
//                            {
//                                Process exeProcess = Process.Start(correctedPath);
//                                Thread.Sleep(TimeSpan.FromMinutes(1));
//                                exeProcess?.Close();
//                            }
//                        }
//                    }
//                    Application.Exit();
//                }
//                catch (Exception ex)
//                {
//                    // Adicionando mais detalhes ao tratamento de exceções
//                    notifyIcon1.ShowBalloonTip(1000, "Jabil Notification Manager", $"Erro ao iniciar o executável: {ex.Message}", ToolTipIcon.Error);
//                    Application.Exit();
//                }
//            }
//            else
//            {
//                notifyIcon1.ShowBalloonTip(1000, "Jabil Notification Manager", "Sem ações ou treinamentos pendentes.", ToolTipIcon.None);
//                Application.Exit();
//            }
//        }


//        public void MainConnection()
//        {
//            var keepRetrying = true;
//            string logonServer = Environment.GetEnvironmentVariable("LOGONSERVER");
//            if (!string.IsNullOrEmpty(logonServer) && logonServer.StartsWith("\\\\"))
//            {
//                logonServer = logonServer.Substring(2); // Remove the initial "\\"
//            }
//            while (keepRetrying)
//            {
//                if (PingHost(logonServer))
//                {
//                    keepRetrying = false;
//                    AutoStart();
//                }
//                else
//                {
//                    keepRetrying = true;
//                    Thread.Sleep(TimeSpan.FromMinutes(5));

//                }
//            }
//        }
//        public static bool PingHost(string nameOrAddress)
//        {
//            bool pingable = false;
//            Ping pinger = null;

//            try
//            {
//                pinger = new Ping();
//                PingReply reply = pinger.Send(nameOrAddress);
//                pingable = reply.Status == IPStatus.Success;
//            }
//            catch (PingException)
//            {               
//            }
//            finally
//            {
//                if (pinger != null)
//                {
//                    pinger.Dispose();
//                }
//            }

//            return pingable;
//        }
//    }
//}
