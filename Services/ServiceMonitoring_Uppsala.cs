using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Services
{
   public class ServiceMonitoring_Uppsala:IServiceMonitoring
    {
        string subject = string.Empty;
        string body = string.Empty;
        List<string> mailrecipents = new List<string>();

        public List<string> GetApplicationServices(string applicationname)
        {
            
        

            throw new NotImplementedException();
        }

        public Dictionary<string, string> CheckServicesStatus(List<string> services)
        {
            subject = "MLL Support Team Monitoring";

            var dic = new Dictionary<string, string>();
            


            foreach (var servicename in services)
            {
                string Status = GetServiceStatus(servicename);

                dic.Add(servicename, Status);
                if (!Status.Contains("Error"))
                {
                    if (Status.Contains("Stopped"))
                    {

                        body = "Service STOP Encountered, initiating service RESTART procedure DO NOT ATTEMPT TO RESTART SERVICES MANUALLY";

                        SendMail(subject, body, mailrecipents);
                        //teststop();
                    }
                }
                else
                {

                    body = "Exception occured : start services manually";
                    SendMail(subject, body, mailrecipents);
                }
            }
            return dic;
           
            
        }

        public bool SendMail(string subject, string body, List<string> mailreceipents)
        {   // richTextBox1.Clear();
           // CheckStatus();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.na.jnj.com");
            mail.From = new MailAddress("NFernan1@its.jnj.com");
            //mail.To.Add("NFernan1@its.jnj.com, RPawaska @ITS.JNJ.com, ABaner36 @its.jnj.com, apatil35 @ITS.JNJ.com, RKadam1@its.jnj.com, MPatil11@its.jnj.com, DAgnihot@ITS.JNJ.com, BGopikri@ITS.JNJ.com"); 
            mail.To.Add("NFernan1@its.jnj.com, RPawaska @ITS.JNJ.com");
            mail.Subject = "";
            // mail.Body = label2.Text + Environment.NewLine + richTextBox1.Text;
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
            throw new NotImplementedException();
        }

        public string GetServiceStatus(string servicename)
        {
            string result = string.Empty;

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
                
                    string serviceStatusQuery = $"SELECT * FROM Win32_Service WHERE Name ={servicename}"; 

                
                    //Console.WriteLine("Inside Try");
                    // Run a Query to Select Service from Win32
                    ObjectQuery query = new ObjectQuery(serviceStatusQuery); // Add Service Name here
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                    ManagementObjectCollection queryCollection = searcher.Get();

                    foreach (ManagementObject queryObj in queryCollection)
                    {

                        result = "Service: " + queryObj["Name"].ToString() + " Status: " + queryObj["State"].ToString();

                    }

                    return result;
                }
            
            catch (Exception em)
            {
                throw new Exception("Service Status Not Found, Check Manually");

            }
            
        }
        
        //starts the service passed to this function
         public bool StartService(string servicename)
        {
            /* string[] test = new string[4] { "'Service4'", "'Service3'", "'Service2'", "'Service1'" };


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
         */
            return false;
            
        }

        //stops the service passed to this function
    
       public bool StopService(string servicename)
        {
            /* string[] test = new string[4] { "'Service1'", "'Service2'", "'Service3'", "'Service4'" };

             if (GetStatus(test[0]) == "Running")
             {
                 StopService(test[0]);
             }
             if (GetStatus(test[0]) == "Stopped" && getstatus(test[1]) == "Running")
             {
                 StopService(test[1]);
             }
             if (GetStatus(test[1]) == "Stopped" && getstatus(test[2]) == "Running")
             {
                 StopService(test[2]);
             }
             if (GetStatus(test[2]) == "Stopped" && getstatus(test[3]) == "Running")
             {
                 StopService(test[3]);
             }

             SendMail("Service STOP procedure completed, initiating service START procedure");
             teststart(); */

            return false;
           
        }
    }
}
