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
			string serverName,
			string applicationName, 
			string serviceName,string status )
		{
			if (serverName == null)
			{
				throw new ArgumentNullException(nameof(serverName));
			}

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

			ServerName = serverName;
			ApplicationName = applicationName;
			ServiceName = serviceName;
			Status = status;
		}

		public string ServerName { get; }
		public string ApplicationName { get; }
		public string ServiceName { get; }
		public string Status { get; }
	}
}
