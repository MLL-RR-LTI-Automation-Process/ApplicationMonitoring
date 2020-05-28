using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Entities;

namespace Services.Interfaces
{
	public interface ICSVWriterService
	{
		bool Save( List<ApplicationServicesStatusModel> applicationServicesStatusModels, string path );
	}
}
