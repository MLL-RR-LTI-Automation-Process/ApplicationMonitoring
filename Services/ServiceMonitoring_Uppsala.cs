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
		List<string> applicationNames = new List<string>();
		Dictionary<string, List<string>> servicesOfApplications = new Dictionary<string, List<string>>();
		Dictionary<string, List<string>> mailrecipentsOfApplication = new Dictionary<string, List<string>>();
		private readonly ICsvReaderService csvReaderService;

		public ServiceMonitoring_Uppsala( ICsvReaderService csvReaderService,string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentException("message", nameof(filePath));
			}

			this.csvReaderService = csvReaderService ?? throw new ArgumentNullException(nameof(csvReaderService));
			ParseCsvFile(filePath);
		}

		private void ParseCsvFile( string filePath )
		{
			var recordList = this.csvReaderService.ReadCsvFileToEmployeeModel(filePath);

			foreach (var serviceModel in recordList)
			{
				var applicationName = serviceModel.ApplicationName;
				applicationNames.Add(applicationName);
				ParseServicesOfApplication(applicationName, serviceModel.Services);
				ParseMailReceipentsOfApplication(applicationName,serviceModel.Emails);
			}
		}

		private void ParseMailReceipentsOfApplication( string applicationName, string emails )
		{
			var listOfEmails = emails.Split(',').ToList();
			mailrecipentsOfApplication.Add(applicationName, listOfEmails);
		}

		private void ParseServicesOfApplication( string applicationName, string services )
		{
			var listOfServices = services.Split(',').ToList();
			servicesOfApplications.Add(applicationName, listOfServices);

		}



		public Dictionary<string, List<string>> ServicesOfApplications {
			get
			{
				return servicesOfApplications;
			}
				}

		public Dictionary<string, List<string>> MailReceipentsOfApplications
		{
			get
			{
				return mailrecipentsOfApplication;
			}
		}

		public List<string> GetApplicationServices(string applicationname)
        {
			List<string> services;
			servicesOfApplications.TryGetValue(applicationname, out services);
			return services;
        }

		public List<string> GetMailReceipentsForApplication( string applicationname )
		{
			List<string> mailReceipents;
			servicesOfApplications.TryGetValue(applicationname, out mailReceipents);
			return mailReceipents;
		}

		public Dictionary<string, string> CheckServicesStatus(
			string username,
			string passowrd,
			List<string> services, 
			string authority, 
			string serverPath )
        {
            subject = "MLL Support Team Monitoring";

            var dic = new Dictionary<string, string>();
            


            foreach (var servicename in services)
            {
                string Status = GetServiceStatus(
					username,
					passowrd, 
					servicename,
					authority,
					serverPath);

                dic.Add(servicename, Status);
                
            }
            return dic;
           
            
        }

        
        public string GetServiceStatus(
			string username, 
			string passowrd,
			string servicename,
			string authority, 
			string serverPath)
        {
            string result = string.Empty;

            try
            {
                //Connect to the Server
                var connection = new ConnectionOptions();
				//connection.Username = "Admin_RPawaska";       // Username
				//connection.Password = "Qwerty@22020";       // Password
				//connection.Authority = "ntlmdomain:NA";
				//var scope = new ManagementScope(
				//	"\\\\DESKTOP-STNNO\\root\\CIMV2", connection); // Add Server Name here

				connection.Username = username;       // Username
				connection.Password = passowrd;       // Password				
			   connection.Authority = authority;				
				var scope = new ManagementScope(
					serverPath, connection);
				scope.Connect();              
                string serviceStatusQuery = $"SELECT * FROM Win32_Service WHERE Name ={servicename}"; 
                // Run a Query to Select Service from Win32
                    ObjectQuery query = new ObjectQuery(serviceStatusQuery); // Add Service Name here
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                    ManagementObjectCollection queryCollection = searcher.Get();

                    foreach (ManagementObject queryObj in queryCollection)
                    {

                        result =  queryObj["State"].ToString();

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
