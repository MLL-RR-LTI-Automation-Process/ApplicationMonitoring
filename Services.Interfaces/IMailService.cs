using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface IMailService
	{
		bool SendMail( string subject, string body, List<string> mailReceipents,bool bodyHtml );
	}
}
