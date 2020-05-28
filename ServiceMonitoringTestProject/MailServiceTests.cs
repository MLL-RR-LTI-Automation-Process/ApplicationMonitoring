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
	public class MailServiceTests
	{
		[TestMethod()]
		public void SendMailTest()
		{
			var subject = "Test Mail from Service Monitoring";
			var body = "Test Email";
			var mail = "BPodder@its.jnj.com";
			var mails = new List<string> { mail };
			var mailService = new MailService();

			mailService.SendMail(subject, body, mails);
		}
	}
}