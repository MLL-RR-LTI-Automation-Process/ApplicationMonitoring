using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domian.Entities;
using Infrastructure.Data;
using Services;
using Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceMonitoring
{
	class ServiceMonitoringConsole
	{
		private static  ICsvReaderService csvReaderService = new 
			CSvServiceMonitoringReader();
		private static ICSVWriterService cSVWriterService = new 
			CSVServiceMonitoringWriter();
		
		static void Main( string[] args )
		{
			Console.WriteLine("Press enter key to start.....");
			var keyinfo = Console.ReadKey();
			if (keyinfo.Key.Equals(ConsoleKey.Enter))
			{

				Console.WriteLine("User Name:");
				var username = Console.ReadLine();
				if (!string.IsNullOrWhiteSpace(username))
				{

					Console.WriteLine("Password:");
					var password = GetConsolePassword();

					if (!string.IsNullOrWhiteSpace(password))
					{
						Console.WriteLine("Authority:");
						var authority = Console.ReadLine();
						
							var serviceMonitoringRepository = new ServiceMonitoringRepository(
				new CSvServiceMonitoringReader(),
				new CSVServiceMonitoringWriter());
							var serviceMonitoring = new ServiceMonitoring_Uppsala(
								serviceMonitoringRepository,
								@"D:\ApplicationsAndRespectiveServices.csv");
							var controller = new ServiceMonitoringController(serviceMonitoring);
							controller.CheckServicesStatus(
								username,
								password,
								authority,
								@"D:\ServiceMonitoringConsole.csv");
						}


					

				}
			}

		}
		/// <summary>
		/// Gets the console secure password.
		/// </summary>
		/// <returns></returns>
		private static  SecureString GetConsoleSecurePassword()
		{
			SecureString pwd = new SecureString();
			while (true)
			{
				ConsoleKeyInfo i = Console.ReadKey(true);
				if (i.Key == ConsoleKey.Enter)
				{
					break;
				}
				else if (i.Key == ConsoleKey.Backspace)
				{
					pwd.RemoveAt(pwd.Length - 1);
					Console.Write("\b \b");
				}
				else
				{
					pwd.AppendChar(i.KeyChar);
					Console.Write("*");
				}
			}
			return pwd;
		}

		/// <summary>
		/// Gets the console password.
		/// </summary>
		/// <returns></returns>
		private static string GetConsolePassword()
		{
			StringBuilder sb = new StringBuilder();
			while (true)
			{
				ConsoleKeyInfo cki = Console.ReadKey(true);
				if (cki.Key == ConsoleKey.Enter)
				{
					Console.WriteLine();
					break;
				}

				if (cki.Key == ConsoleKey.Backspace)
				{
					if (sb.Length > 0)
					{
						Console.Write("\b\0\b");
						sb.Length--;
					}

					continue;
				}

				Console.Write('*');
				sb.Append(cki.KeyChar);
			}

			return sb.ToString();
		}
	}

		
		
	}
