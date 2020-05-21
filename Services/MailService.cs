using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;

namespace Services
{
	public class MailService :IMailService
	{
		public bool SendMail( string subject, string body, List<string> mailreceipents )
		{   // richTextBox1.Clear();
			// CheckStatus();

			MailMessage mail = new MailMessage();
			SmtpClient SmtpServer = new SmtpClient("smtp.na.jnj.com");
			mail.From = new MailAddress("NFernan1@its.jnj.com");
			//mail.To.Add("NFernan1@its.jnj.com, RPawaska @ITS.JNJ.com, ABaner36 @its.jnj.com, apatil35 @ITS.JNJ.com, RKadam1@its.jnj.com, MPatil11@its.jnj.com, DAgnihot@ITS.JNJ.com, BGopikri@ITS.JNJ.com"); 
			mail.To.Add("NFernan1@its.jnj.com, RPawaska @ITS.JNJ.com");
			mail.Subject = "";
			// mail.Body = label2.Text + Environment.NewLine + richTextBox1.Text;
			SmtpServer.Port = 25;
			SmtpServer.EnableSsl = true;
			try
			{
				SmtpServer.Send(mail);
				Console.WriteLine("Mail sent");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Not sent");
			}
			throw new NotImplementedException();
		}

	}
}
