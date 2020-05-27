using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Entities;

namespace Services.Interfaces
{
   public interface IServiceMonitoring
    {
		bool Save( 
			List<ApplicationServicesStatusModel> applicationServicesStatusModels,
			string path );

		List<string> GetApplicationServices(string applicationname);
        Dictionary<string, string> CheckServicesStatus(
			string username,
			string passowrd,
			string authority, 
			string serverPath,
			List<string> services);
        string GetServiceStatus( 
			string username,
			string passowrd, 
			string servicename, 
			string authority, 
			string serverPath );
        bool StartService(string servicename);
        bool StopService(string servicename);
    }
}
