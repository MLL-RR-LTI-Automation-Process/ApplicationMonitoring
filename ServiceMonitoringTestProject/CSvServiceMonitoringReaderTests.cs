using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
	[TestClass()]
	public class CSvServiceMonitoringReaderTests
	{
		[TestMethod()]
		public void ReadCsvFileToEmployeeModelTest()
		{
			string path = "ApplicationsAndRespectiveServices.csv";
			var csvReader = new CSvServiceMonitoringReader();
			var recordList = csvReader.ReadCsvFileToEmployeeModel(path);
			var firstrecord = recordList[0];
			var applicationName = firstrecord.ApplicationName;
			var services = firstrecord.Services;
			var emails = firstrecord.Emails;

			//Assert
			Assert.AreEqual("San Angelo Downtime Tracker Proficy Historian", applicationName);

			//Services
			var listOfServices = services.Split(',').ToList();
			var firstService = listOfServices[0];
			Assert.AreEqual("IndexingService", firstService);

			//Emails
			var listOfEmails = emails.Split(',').ToList();
			var firstMailreciepents = listOfEmails[0];
			Assert.AreEqual("BPodder@its.jnj.com", firstMailreciepents);


		}
	}
}