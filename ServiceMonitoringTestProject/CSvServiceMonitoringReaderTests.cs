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
		public void ReadCsvFileToServiceMonitoringModelTest()
		{
			string path = "ApplicationsAndRespectiveServices.csv";
			var csvReader = new CSvServiceMonitoringReader();
			var recordList = csvReader.ReadCsvFileToServiceMonitoringModel(path);
			var firstrecord = recordList[0];
			var serverName = firstrecord.ServerName;
			var applicationName = firstrecord.ApplicationName;
			var services = firstrecord.Services;
			var emails = firstrecord.Emails;

			//Assert
			Assert.AreEqual("Amazon SSM Agent", applicationName);

			//Services
			var listOfServices = services.Split(',').ToList();
			var firstService = listOfServices[0];
			Assert.AreEqual("AmazonSSMAgent", firstService);

			//Emails
			var listOfEmails = emails.Split(',').ToList();
			var firstMailreciepents = listOfEmails[0];
			Assert.AreEqual("BPodder@its.jnj.com", firstMailreciepents);


		}
	}
}