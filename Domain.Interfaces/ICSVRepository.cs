using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Entities;

namespace Domain.Interfaces
{
	public interface IServiceMonitoringRepository
	{
		bool Save( 
			List<ApplicationServicesStatusModel> applicationServicesStatusModels, 
			string path);
		List<ServiceMonitoringModel> Get(string path);

	}
}
