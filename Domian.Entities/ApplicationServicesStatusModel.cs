using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Entities
{
	public class ApplicationServicesStatusModel
	{


		public ApplicationServicesStatusModel(
			string applicationName, 
			string serviceName,string status )
		{
			if (applicationName == null)
			{
				throw new ArgumentNullException(nameof(applicationName));
			}

			if (serviceName == null)
			{
				throw new ArgumentNullException(nameof(serviceName));
			}

			if (status == null)
			{
				throw new ArgumentNullException(nameof(status));
			}

			ApplicationName = applicationName;
			ServiceName = serviceName;
			Status = status;
		}

		public string ApplicationName { get; }
		public string ServiceName { get; }
		public string Status { get; }
	}
}
