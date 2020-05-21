using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Entities;

namespace Services.Interfaces
{
	public interface ICsvReaderService
	{
		List<ServiceMonitoringModel> ReadCsvFileToEmployeeModel( string path );
	}
}
