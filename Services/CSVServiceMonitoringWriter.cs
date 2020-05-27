using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Domian.Entities;
using Services.Interfaces;

namespace Services
{
	public class CSVServiceMonitoringWriter : ICSVWriterService
	{
		
		public bool Save( List<ApplicationServicesStatusModel> applicationServicesStatusModels, string path )
		{
			if (applicationServicesStatusModels == null)
			{
				throw new ArgumentNullException(nameof(applicationServicesStatusModels));
			}

			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			try
			{
				using (StreamWriter streamWriter = new StreamWriter(path, false, new UTF8Encoding(true)))
				using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
				{
					csvWriter.WriteHeader<ApplicationServicesStatusModel>();
					csvWriter.NextRecord();
					foreach (ApplicationServicesStatusModel applicationServicesStatusModel in applicationServicesStatusModels)
					{
						csvWriter.WriteRecord<ApplicationServicesStatusModel>(applicationServicesStatusModel);
						csvWriter.NextRecord();
					}


				}
				return true;
			}
			catch (UnauthorizedAccessException e)
			{
				throw new Exception(e.Message);
			}
			catch (FieldValidationException e)
			{
				throw new Exception(e.Message);
			}
			catch (CsvHelperException e)
			{
				throw new Exception(e.Message);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}
	}
}
