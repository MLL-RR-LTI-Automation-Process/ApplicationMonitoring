using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domian.Entities;
using Services.Interfaces;

namespace Infrastructure.Data
{
	public class ServiceMonitoringRepository : IServiceMonitoringRepository
	{
		private readonly ICsvReaderService csvReaderService;
		private readonly ICSVWriterService cSVWriterService;

		public ServiceMonitoringRepository( 
			ICsvReaderService csvReaderService,
			ICSVWriterService cSVWriterService)
		{
			if (csvReaderService == null)
			{
				throw new ArgumentNullException(nameof(csvReaderService));
			}

			if (cSVWriterService == null)
			{
				throw new ArgumentNullException(nameof(cSVWriterService));
			}

			this.csvReaderService = csvReaderService;
			this.cSVWriterService = cSVWriterService;
		}
		public List<ServiceMonitoringModel> Get( string path )
		{
			var data = this.csvReaderService.ReadCsvFileToServiceMonitoringModel(path);
			return data;
		}

		public bool Save(
			List<ApplicationServicesStatusModel> applicationServicesStatusModels,
			string path )
		{
			try
			{
				this.cSVWriterService.Save(applicationServicesStatusModels, path);
				return true;
			}
			catch (Exception ex)
			{

				throw new Exception(ex.Message);
			}
			
		}
	}
}
