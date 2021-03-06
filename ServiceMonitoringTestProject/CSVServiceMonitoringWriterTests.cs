﻿using Domian.Entities;
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
	public class CSVServiceMonitoringWriterTests
	{
		[TestMethod()]
		public void SaveTest()
		{
			var firstApplicationName = "Adobe";
			var serverName = "DESKTOP-1FTRA6H";
			  var firstApplicationServicesStatusModel = new ApplicationServicesStatusModel(
				  serverName,
				  firstApplicationName,
				"AdobeARMservice", 
				"Running");
			var secondApplicationServicesStatusModel = new ApplicationServicesStatusModel(
				 serverName,
				firstApplicationName,
				"AdobeFlashPlayerUpdateSvc",
				"Stopped");


			var secondApplicationName = "Amazon SSM Agent";
			var dict2 = new Dictionary<string, string>();

			var thirdApplicationServicesStatusModel = new ApplicationServicesStatusModel(
				 serverName,
				secondApplicationName,
				"AmazonSSMAgent",
				"Running");
			
			
			var thirdApplicationName = "Application Identity";
			var fourthApplicationServicesStatusModel = new ApplicationServicesStatusModel(
				 serverName,
				thirdApplicationName,
				"AppIDSvc", 
				"Running");
			
			

			var applicationServicesStatusModels = new List<ApplicationServicesStatusModel>();
			applicationServicesStatusModels.Add(firstApplicationServicesStatusModel);
			applicationServicesStatusModels.Add(secondApplicationServicesStatusModel);
			applicationServicesStatusModels.Add(thirdApplicationServicesStatusModel);
			applicationServicesStatusModels.Add(fourthApplicationServicesStatusModel);

			var path = @"D:\ServiceMonitoringStatus.csv";
			var csvWriter = new CSVServiceMonitoringWriter();
			csvWriter.Save(applicationServicesStatusModels, path);

		}
	}
}