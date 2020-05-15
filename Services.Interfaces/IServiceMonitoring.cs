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
        Dictionary<string, string> CheckServicesStatus(List<string> services);
        bool SendMail(string subject,string body, List<string> mailreceipents);
        string GetStatus(string servicename);
        bool StartService(string servicename);
        bool StopService(string servicename);
    }
}
