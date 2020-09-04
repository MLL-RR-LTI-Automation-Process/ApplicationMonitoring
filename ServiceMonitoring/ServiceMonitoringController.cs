using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Entities;
using Services.Interfaces;

namespace ServiceMonitoring
{
	public class ServiceMonitoringController
	{
		private readonly IServiceMonitoring serviceMonitoring;
		private readonly IMailService mailService;

		public ServiceMonitoringController(
			IServiceMonitoring serviceMonitoring,
			IMailService mailService)
		{
			if (serviceMonitoring == null)
			{
				throw new ArgumentNullException(nameof(serviceMonitoring));
			}

			if (mailService == null)
			{
				throw new ArgumentNullException(nameof(mailService));
			}

			this.serviceMonitoring = serviceMonitoring;
			this.mailService = mailService;
		}

		public void CheckServicesStatus(
			string username,
			string passowrd,
			string authority,
			string savetoPath)
		{
			var listofApplicationServicesStatusModel = new List
				<ApplicationServicesStatusModel>();
			foreach (var record in serviceMonitoring.AllRecords)
			{
				var firstRecord = serviceMonitoring.AllRecords[0];
				var servername = firstRecord.Item1;
				var applicationName = firstRecord.Item2;
				var services = firstRecord.Item3;
				var mails = firstRecord.Item4;
                foreach (var service in services)
                {
                    var status = this.serviceMonitoring.GetServiceStatus(
                        username,
                        passowrd,
                        authority,
                        servername,
                        service);
                    var applicationServicesStatusModel = new ApplicationServicesStatusModel(
                        servername,
                        applicationName,
                        service,
                        status);
                    listofApplicationServicesStatusModel.Add(applicationServicesStatusModel);
                    if (status.Equals("Stopped"))
                    {
                        var subject = $"{service} is stopped for application {applicationName} on server {servername}";
                        var body = "This is a notification mail to check service status";
                        mailService.SendMail(subject, body, mails, false);
                    }
                }

            }

			this.serviceMonitoring.Save(listofApplicationServicesStatusModel, savetoPath);
		}
	}
}
