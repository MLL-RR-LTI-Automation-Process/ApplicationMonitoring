using Domian.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoring
{
  internal  class ScheduleReport
    {
        private readonly string shift;
        private readonly IServiceMonitoring serviceMonitoring;
        private readonly IMailService mailService;
        public ScheduleReport(
            string shift,
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

            this.shift = shift ?? throw new ArgumentNullException(nameof(shift));
            this.serviceMonitoring = serviceMonitoring;
            this.mailService = mailService;
        }

        public void CheckServicesStatus(
            string username,
            string passowrd,
            string authority)
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

                   
                    ApplicationServicesStatusModel applicationServicesStatusModel = new 
                        ApplicationServicesStatusModel(
                        record.Item1,
                        applicationName,
                        service,
                        status);
                    listofApplicationServicesStatusModel.Add(applicationServicesStatusModel);
                    var report = CreateReport(listofApplicationServicesStatusModel);
                    var subject = $"Automated {shift} for Collabration FM";
                    
                    mailService.SendMail(subject, report, firstRecord.Item4, true);
                }

            }

        
        }

        private string CreateReport(List<ApplicationServicesStatusModel> listofApplicationServicesStatusModel)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"
<!DOCTYPE html>
            <html>
            <head>
            <style>

 table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
table {
  border-spacing: 5px;
}

</style>
</head>
<body>
<table >

<th>Server Name</th>
<th>Application Name</th>
<th>Service Name</th>
<th>Status</th>


");

            foreach (var model in listofApplicationServicesStatusModel)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", model.ServerName);
                sb.AppendFormat("<td>{0}</td>", model.ApplicationName);
                sb.AppendFormat("<td>{0}</td>", model.ServiceName);
                sb.AppendFormat("<td>{0}</td>", model.Status);
                sb.Append("</tr>");
            }


            sb.Append("</table></body></html>");

            return sb.ToString();
        }
    }
}
