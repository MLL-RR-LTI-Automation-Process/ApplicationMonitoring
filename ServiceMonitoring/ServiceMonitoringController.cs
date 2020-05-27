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

		public ServiceMonitoringController(
			IServiceMonitoring serviceMonitoring)
		{
			if (serviceMonitoring == null)
			{
				throw new ArgumentNullException(nameof(serviceMonitoring));
			}

			this.serviceMonitoring = serviceMonitoring;
		}

		public void CheckServicesStatus(
			string username,
			string passowrd,
			string authority,
			string serverPath,
			string savetoPath)
		{
			var listofApplicationServicesStatusModel = new List
				<ApplicationServicesStatusModel>();
			foreach (var application in this.serviceMonitoring.ServicesOfApplications.Keys)
			{
				var services = this.serviceMonitoring.GetApplicationServices(application);
				foreach (var service in services)
				{
					var status = this.serviceMonitoring.GetServiceStatus(
						username,
						passowrd,
						authority,
						serverPath,
						service);
					var applicationServicesStatusModel = new ApplicationServicesStatusModel(
						application,
						service,
						status);
					listofApplicationServicesStatusModel.Add(applicationServicesStatusModel);
				}

			}

			this.serviceMonitoring.Save(listofApplicationServicesStatusModel, savetoPath);
		}
	}
}
