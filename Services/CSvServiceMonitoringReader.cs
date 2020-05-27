using CsvHelper;
using Domian.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace Services
{
	public class CSvServiceMonitoringReader : ICsvReaderService
	{
		public List<ServiceMonitoringModel> ReadCsvFileToEmployeeModel( string path )
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			try
			{
				using (var reader = new StreamReader(path, Encoding.Default))
				using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
				{
					var records = csv.GetRecords<ServiceMonitoringModel>().ToList();
					return records;
				}
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
