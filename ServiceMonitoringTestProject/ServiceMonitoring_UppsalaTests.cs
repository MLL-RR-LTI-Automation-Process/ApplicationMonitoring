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

			Assert.Fail();
		}

		[TestMethod()]
		public void GetServiceStatusTest()
		{
			Assert.Fail();
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