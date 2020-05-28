using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net;
using System.Net.Mail;


namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Initializes timer so code will re run after every 30 mins
        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = (1800 * 1000); // 30 mins - 1800
            timer.Tick += new EventHandler(button1_Click_1);
            timer.Start();
        }

        //to display service status
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //will update system date time on dashboard and call checkstatus()
        private void button1_Click_1(object sender, EventArgs e)
        {
            label2.Text = System.DateTime.Now.ToString();
            richTextBox1.Text = String.Empty;
            CheckStatus();
            CheckIfStop();
            //displaystatus();
            //teststop();
            //displaystatus();
            //teststart();
        }

        //will connect with remote server and check status of each service and update the same in text box on dashboard
        private void CheckStatus()
        {
            string[] data = new string[4] { "", "", "", "" };

            try
            {
                //Connect to the Server
                var connection = new ConnectionOptions();
                connection.Username = "";       // Username
                connection.Password = "";       // Password

                connection.Authority = "ntlmdomain:NA";
                var scope = new ManagementScope(
                    "\\\\DESKTOP-UBLFJK5\\root\\CIMV2", connection); // Add Server Name here
                scope.Connect();

                //Console.WriteLine("Connected");

                //Connection to the server opened
                string[] result = new string[4] { "", "", "", "" };
                //data = new string[4] { "'smpbackSPR1'", "'smplock'", "'smpSPR1'", "'smwSPR1'" };
                data = new string[4] { "'Service1'", "'Service2'", "'Service3'", "'Service4'" };
                string fp = "SELECT * FROM Win32_Service WHERE Name = ";
                for (int i = 0; i <= 3; i++)
                {
                    string qtp = string.Concat(fp, data[i]); //fp + data[i];

                    try
                    {
                        //Console.WriteLine("Inside Try");
                        // Run a Query to Select Service from Win32
                        ObjectQuery query = new ObjectQuery(qtp); // Add Service Name here
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                        ManagementObjectCollection queryCollection = searcher.Get();

                        foreach (ManagementObject queryObj in queryCollection)
                        {

                            result[i] = "Service: " + queryObj["Name"].ToString() + " Status: " + queryObj["State"].ToString();

                        }

                    }

                    catch (ManagementException er)
                    {

                        Console.WriteLine("An error occurred while checking services status" + er.Message);
                        result[i] = "An error occurred while checking services status:(Error)" + er.Message;

                    }

                }

                foreach (string item in result)
                {
                    richTextBox1.Text += item + "\n\r";
                }


            }
            catch (Exception em)
            {
                richTextBox1.Text = "An error occurred while connecting to the server " + em.Message;

            }
        }

        //will check if service is stopped, if stopped encountered then it will call teststop()
        private void CheckIfStop()
        {
            String Status = richTextBox1.Text;

            if (!Status.Contains("Error"))
            {
                if (Status.Contains("Stopped"))
                {
                    SendMail("Service STOP Encountered, initiating service RESTART procedure DO NOT ATTEMPT TO RESTART SERVICES MANUALLY");
                    teststop();
                }
            }
            else
            {
                richTextBox1.Text += "\n\r" + "Please Start the Services Manually ";
                SendMail("Exception occured : start services manually");
            }

        } 

        //will call ServiceStop() according to condition 
        private void teststop()
        {
            string[] test = new string[4] { "'Service1'", "'Service2'", "'Service3'", "'Service4'" };

            if (getstatus(test[0]) == "Running")
            {
                StopService(test[0]);
            }
            if (getstatus(test[0]) == "Stopped" && getstatus(test[1]) == "Running")
            {
                StopService(test[1]);
            }
            if (getstatus(test[1]) == "Stopped" && getstatus(test[2]) == "Running")
            {
                StopService(test[2]);
            }
            if (getstatus(test[2]) == "Stopped" && getstatus(test[3]) == "Running")
            {
                StopService(test[3]);
            }

            SendMail("Service STOP procedure completed, initiating service START procedure");
            teststart();
        }

        //will call ServiceStart() according to condition
        private void teststart()
        {
            string[] test = new string[4] { "'Service4'", "'Service3'", "'Service2'", "'Service1'" };


            if (getstatus(test[0]) == "Stopped")
            {
                StartService(test[0]);
            }
            if (getstatus(test[0]) == "Running" && getstatus(test[1]) == "Stopped")
            {
                StartService(test[1]);
            }
            if (getstatus(test[1]) == "Running" && getstatus(test[2]) == "Stopped")
            {
                StartService(test[2]);
            }
            if (getstatus(test[2]) == "Running" && getstatus(test[3]) == "Stopped")
            {
                StartService(test[3]);
            }

            SendMail("Service START procedure completed, below is the current status of services");
        }

        //will return the service status of the service name passed to this function
        public static string getstatus(string servicename)
        {
            try
            {
                //Connect to the Server
                var connection = new ConnectionOptions();
                connection.Username = "";       // Username
                connection.Password = "";       // Password
                connection.Authority = "ntlmdomain:NA";
                var scope = new ManagementScope("\\\\DESKTOP-UBLFJK5\\root\\CIMV2", connection); // Add Server name here 
                scope.Connect();
                string fp = "SELECT * FROM Win32_Service WHERE Name = ";
                string qtp = string.Concat(fp, servicename); //fp + data[i];
                string result = "default";
                try
                {
                    Console.WriteLine("Inside Try");
                    // Run a Query to Select Service from Win32
                    ObjectQuery query = new ObjectQuery(qtp); // Add Service Name here
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                    ManagementObjectCollection queryCollection = searcher.Get();

                    foreach (ManagementObject queryObj in queryCollection)
                    {
                        result = queryObj["State"].ToString();
                    }
                    return result;
                }
                catch (ManagementException er)
                {
                    Console.WriteLine("An error occurred while checking services status" + er.Message);
                    result = "An error occurred while checking services status:(Error)" + er.Message;
                    return result;
                }
            }
            catch (Exception em)
            {
                string testexception = "An error occurred while connecting to the server " + em.Message;
                return testexception;
            }
        }

        //stops the service passed to this function
        private static void StopService(string svcName)
        {
            try
            {
                #region Code to start the service

                string serviceName = svcName;
                string IP = "DESKTOP-UBLFJK5";
                string username = "";
                string password = "";
                ConnectionOptions connectoptions = new ConnectionOptions();
                connectoptions.Username = username;
                connectoptions.Password = password;
                ManagementScope scope = new ManagementScope(@"\\" + IP + @"\root\cimv2");
                scope.Options = connectoptions;
                SelectQuery query = new SelectQuery("select * from Win32_Service where name = " + serviceName + "");

                using (ManagementObjectSearcher searcher = new
                            ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject service in collection)
                    {
                        if (service["Started"].Equals(true))
                        {
                            //Stop the service
                            service.InvokeMethod("StopService", null);
                        }

                        while (getstatus(svcName) == "Running")
                        {
                            //do nothing
                            Timer timer1 = new Timer();
                            timer1.Interval = (120 * 1000); //wait 2 mins
                            timer1.Start();
                        }
                        // Console.WriteLine(svcName+" stopped");
                    }
                }

                #endregion

            }
            catch (NullReferenceException)
            {

            }
        }

        //starts the service passed to this function
        private static void StartService(string svcName)
        {
            try
            {

                #region Code to start the service

                string serviceName = svcName;
                string IP = "DESKTOP-UBLFJK5";
                string username = "";
                string password = "";

                ConnectionOptions connectoptions = new ConnectionOptions();
                connectoptions.Username = username;
                connectoptions.Password = password;
                ManagementScope scope = new ManagementScope(@"\\" + IP + @"\root\cimv2");
                scope.Options = connectoptions;
                SelectQuery query = new SelectQuery("select * from Win32_Service where name = " + serviceName + "");

                using (ManagementObjectSearcher searcher = new
                            ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject service in collection)
                    {
                        if (service["Started"].Equals(false))
                        {
                            //Stop the service
                            service.InvokeMethod("StartService", null);
                        }

                        while (getstatus(svcName) == "Stopped")
                        {
                            //do nothing
                            Timer timer1 = new Timer();
                            timer1.Interval = (120 * 1000); //wait 2 mins
                            timer1.Start();
                        }
                        //Console.WriteLine(svcName + " started");
                    }
                }

                #endregion

            }
            catch (NullReferenceException)
            {

            }
        }

        //send manually with service status
        private void button2_Click(object sender, EventArgs e)
        {

            SendMail("Service Status for Samplemanager");

        }

        //send mail function with subject passed as parameter string
        private void SendMail(string mailhead)
        {
            richTextBox1.Clear();
            CheckStatus();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.na.jnj.com");
            mail.From = new MailAddress("NFernan1@its.jnj.com");
            //mail.To.Add("NFernan1@its.jnj.com, RPawaska @ITS.JNJ.com, ABaner36 @its.jnj.com, apatil35 @ITS.JNJ.com, RKadam1@its.jnj.com, MPatil11@its.jnj.com, DAgnihot@ITS.JNJ.com, BGopikri@ITS.JNJ.com"); 
            mail.To.Add("NFernan1@its.jnj.com, RPawaska @ITS.JNJ.com");
            mail.Subject = mailhead;
            mail.Body = label2.Text + Environment.NewLine + richTextBox1.Text;
            SmtpServer.Port = 25;
            SmtpServer.EnableSsl = true;
            try
            {
                SmtpServer.Send(mail);
                Console.WriteLine("Mail sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not sent");
            }
        }


    }
}

