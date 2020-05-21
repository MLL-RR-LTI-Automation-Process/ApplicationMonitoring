using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace Domian.Entities
{
	public class ServiceMonitoringModel
	{
		public string ApplicationName { get; set; }
		public string Services { get; set; }
		public string Emails { get; set; }
	}
}
