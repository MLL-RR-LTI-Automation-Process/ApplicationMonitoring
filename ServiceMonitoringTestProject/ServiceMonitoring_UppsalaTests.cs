using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using Domian.Entities;

namespace Services.Tests
{
	[TestClass()]
	public class ServiceMonitoring_UppsalaTests
	{
		[TestMethod()]
		public void ServiceMonitoring_UppsalaTest_CheckNull_ShouldThrowException()
		{
			Action action = () => new ServiceMonitoring(null, null);
			action.Should().Throw<ArgumentException>();

		}

		[TestMethod()]
		public void GetApplicationServicesTest_shouldGetServicesOfApplication_ExpecteTrue()
		{
			var serviceMonitoringRepository = new ServiceMonitoringRepository(
				new CSvServiceMonitoringReader(),
				new CSVServiceMonitoringWriter());
			var serviceMonitoring = new ServiceMonitoring(
				serviceMonitoringRepository,
				"ApplicationsAndRespectiveServices.csv");
			var services = serviceMonitoring.GetApplicationServices(
				"San Angelo Downtime Tracker Proficy Historian");
			services.Should().HaveCountGreaterThan(0);
		}

		[TestMethod()]

		public void GetAllRecords_ShouldGetOneRowDataConsistServerNameApplicationNameServicesMails()
		{
			var serviceMonitoringRepository = new ServiceMonitoringRepository(
				new CSvServiceMonitoringReader(),
				new CSVServiceMonitoringWriter());
			var serviceMonitoring = new ServiceMonitoring(
				serviceMonitoringRepository,
				"ApplicationsAndRespectiveServices.csv");
			serviceMonitoring.AllRecords.Should().HaveCountGreaterThan(0);
			var firstRecord = serviceMonitoring.AllRecords[0];
			var servername = firstRecord.Item1;
			var applicationName = firstRecord.Item2;
			var services = firstRecord.Item3;
			var mails = firstRecord.Item4;

			services.Should().HaveCountGreaterThan(0);
			mails.Should().HaveCountGreaterThan(0);
		}

		[TestMethod()]
		public void GetMailReceipentsForApplicationTest()
		{
			var serviceMonitoringRepository = new ServiceMonitoringRepository(
				new CSvServiceMonitoringReader(),
				new CSVServiceMonitoringWriter());
			var serviceMonitoring = new ServiceMonitoring(
				serviceMonitoringRepository,
				"ApplicationsAndRespectiveServices.csv");
			var mailReceipents = serviceMonitoring.GetMailReceipentsForApplication(
					"San Angelo Downtime Tracker Proficy Historian");
			mailReceipents.Should().HaveCountGreaterThan(0);
		}

		[TestMethod()]
		public void CheckServicesStatusTest()
		{

			var serviceMonitoringRepository = new ServiceMonitoringRepository(
					new CSvServiceMonitoringReader(),
					new CSVServiceMonitoringWriter());
			var serviceMonitoring = new ServiceMonitoring(
				serviceMonitoringRepository,
				"ApplicationsAndRespectiveServices.csv");

			var allServicesWithApplications = serviceMonitoring.ServicesOfApplications;
			var servicesForAmazon = allServicesWithApplications["Amazon SSM Agent"];
			string username = "Admin_NFernan1";
			string passowrd = "";
			string authority = "ntlmdomain:NA";
			string serverPath = @"\\DESKTOP-1FTRA6H\root\CIMV2";
			var serviceswithStaus = serviceMonitoring.CheckServicesStatus(
				username,
				passowrd,
				authority,
				serverPath,
				servicesForAmazon);
		}

		[TestMethod()]
		public void GetServiceStatusTest()
		{
			var serviceMonitoringRepository = new ServiceMonitoringRepository(
				new CSvServiceMonitoringReader(),
				new CSVServiceMonitoringWriter());
			var serviceMonitoring = new ServiceMonitoring(
				serviceMonitoringRepository,
				"ApplicationsAndRespectiveServices.csv");
			string servicename = "'AmazonSSMAgent'";
			string username = "Admin_NFernan1";
			string passowrd = "";
			string authority = "ntlmdomain:NA";
			string serverPath = @"\\DESKTOP-1FTRA6H\root\CIMV2";
			serviceMonitoring.GetServiceStatus(
				username,
				passowrd,
				servicename,
				authority,
				serverPath);

		}

		[TestMethod()]
		public void SaveTest()
		{
			var firstApplicationName = "Adobe";
			var serverName = "DESKTOP-1FTRA6H";
			var serviceMonitoringRepository = new ServiceMonitoringRepository(
				new CSvServiceMonitoringReader(),
				new CSVServiceMonitoringWriter());
			var serviceMonitoring = new ServiceMonitoring(
				serviceMonitoringRepository,
				"ApplicationsAndRespectiveServices.csv");
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
			var applicationServicesStatusModels = new List<ApplicationServicesStatusModel>();
			applicationServicesStatusModels.Add(firstApplicationServicesStatusModel);
			applicationServicesStatusModels.Add(secondApplicationServicesStatusModel);
			var path = @"D:\ServiceMonitoringStatus_2.csv";
			serviceMonitoring.Save(applicationServicesStatusModels, path);
		}

		[TestMethod()]
		public void StartServiceTest()
		{
			Assert.Fail();
		}

		[TestMethod()]
		public void StopServiceTest()
		{
			Assert.Fail();
		}

		
	}
}