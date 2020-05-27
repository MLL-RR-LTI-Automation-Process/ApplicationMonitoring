using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Tests
{
	[TestClass()]
	public class ServiceMonitoring_UppsalaTests
	{
		[TestMethod()]
		public void ServiceMonitoring_UppsalaTest_CheckNull_ShouldThrowException()
		{
			Action action = () => new ServiceMonitoring_Uppsala(null, null);
			action.Should().Throw<ArgumentException>();

		}

		[TestMethod()]
		public void GetApplicationServicesTest_shouldGetServicesOfApplication_ExpecteTrue()
		{
			var serviceMonitoring = new ServiceMonitoring_Uppsala(
				new CSvServiceMonitoringReader(),
				"ApplicationsAndRespectiveServices.csv");
			var services = serviceMonitoring.GetApplicationServices(
				"San Angelo Downtime Tracker Proficy Historian");
			services.Should().HaveCountGreaterThan(0);
		}

		[TestMethod()]
		public void GetMailReceipentsForApplicationTest()
		{
			var serviceMonitoring = new ServiceMonitoring_Uppsala(
				new CSvServiceMonitoringReader(),
				"ApplicationsAndRespectiveServices.csv");
			var mailReceipents = serviceMonitoring.GetMailReceipentsForApplication(
					"San Angelo Downtime Tracker Proficy Historian");
			mailReceipents.Should().HaveCountGreaterThan(0);
		}

		[TestMethod()]
		public void CheckServicesStatusTest()
		{

			var serviceMonitoring = new ServiceMonitoring_Uppsala(
		  new CSvServiceMonitoringReader(),
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
			var serviceMonitoring = new ServiceMonitoring_Uppsala(
		  new CSvServiceMonitoringReader(),
		  "ApplicationsAndRespectiveServices.csv");
			string servicename = "'AmazonSSMAgent'";
			string username = "Admin_NFernan1";
			string passowrd = "Feb@42020";
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