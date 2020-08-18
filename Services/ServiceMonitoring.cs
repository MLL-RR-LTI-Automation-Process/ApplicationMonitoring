using Domain.Interfaces;
using Domian.Entities;
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
   public class ServiceMonitoring:IServiceMonitoring
    {
        string subject = string.Empty;
        string body = string.Empty;
		List<string> applicationNames = new List<string>();

		List<Tuple<string, string, List<string>, List<string>>> allrecords = new 
			List<Tuple<string, string, List<string>, List<string>>>();

			 
		Dictionary<string, List<string>> servicesOfApplications = new Dictionary<string, List<string>>();
		Dictionary<string, List<string>> mailrecipentsOfApplication = new Dictionary<string, List<string>>();
		private readonly IServiceMonitoringRepository serviceMonitoringRepository;

		public ServiceMonitoring( 
			IServiceMonitoringRepository serviceMonitoringRepository, 
			string filePath)
		{
			if (serviceMonitoringRepository == null)
			{
				throw new ArgumentNullException(nameof(serviceMonitoringRepository));
			}

			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentException("message", nameof(filePath));
			}

			this.serviceMonitoringRepository = serviceMonitoringRepository;
			ParseCsvFile(filePath);
			
		}

		private void ParseCsvFile( string filePath )
		{
			var recordList = this.serviceMonitoringRepository.Get(filePath);
			
			foreach (var serviceModel in recordList)
			{
				var serverName = serviceModel.ServerName;
				var applicationName = serviceModel.ApplicationName;
				

				applicationNames.Add(applicationName);
				var services = ParseServicesOfApplication(applicationName, serviceModel.Services);
				var mails = ParseMailReceipentsOfApplication(applicationName,serviceModel.Emails);
				Tuple<string, string, List<string>, List<string>> record = new
					Tuple<string, string, List<string>, List<string>>(
					serverName,
					applicationName,
					services,
					mails);
				allrecords.Add(record);
			}
		}

		private List<string> ParseMailReceipentsOfApplication( string applicationName, string emails )
		{
			var listOfEmails = emails.Split(',').ToList();
			mailrecipentsOfApplication.Add(applicationName, listOfEmails);
			return listOfEmails;
		}

		private List<string> ParseServicesOfApplication( string applicationName, string services )
		{
			var listOfServices = services.Split(',').ToList();
			servicesOfApplications.Add(applicationName, listOfServices);
			return listOfServices;

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

		public List<Tuple<string, string, List<string>, List<string>>> AllRecords
		{
			get
			{
				return allrecords;
			}
		}

		public bool Save( 
			List<ApplicationServicesStatusModel> applicationServicesStatusModels,
			string path)
		{
			var result = this.serviceMonitoringRepository.Save(
				applicationServicesStatusModels,
				path);
			return result;
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
			string authority, 
			string serverPath,
			List<string> services)
        {
          

            var dic = new Dictionary<string, string>();

            foreach (var servicename in services)
            {
				

				string Status = GetServiceStatus(
					username,
					passowrd,
					authority,
					serverPath,
					servicename);

                dic.Add(servicename, Status);
                
            }
            return dic;
           
            
        }

        
        public string GetServiceStatus(
			string username, 
			string passowrd,
			string authority, 
			string serverPath,
			string servicename)
        {
            string result = string.Empty;

            try
            {
                //Connect to the Server
                var connection = new ConnectionOptions();
				
				connection.Username = username;       // Username
				connection.Password = passowrd;       // Password				
			  // connection.Authority = authority;
				var serverAbcolutePath = $"\\\\{serverPath}\\root\\CIMV2";
				var scope = new ManagementScope(
					serverAbcolutePath, connection);
				scope.Connect();
				var serviceNameInQueryFormat = $"'{servicename}'";
				string serviceStatusQuery = $"SELECT * FROM Win32_Service WHERE Name ={serviceNameInQueryFormat}"; 
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
