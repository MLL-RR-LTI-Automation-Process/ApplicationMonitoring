using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IServiceMonitoring
    {
        List<string> GetApplicationServices(string applicationname);
        Dictionary<string, string> CheckServicesStatus(
			string username,
			string passowrd,
			List<string> services,
			string authority, 
			string serverPath );
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
