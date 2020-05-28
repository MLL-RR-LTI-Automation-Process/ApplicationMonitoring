using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
		private static string authority;
		private static System.Timers.Timer myTimer;
		private static string username;
		private static string password;

		static void Main( string[] args )
		{
			Console.WriteLine("Press enter key to start.....");
			var keyinfo = Console.ReadKey();
			if (keyinfo.Key.Equals(ConsoleKey.Enter))
			{

				Console.WriteLine("User Name:");
				 username = Console.ReadLine();
				if (!string.IsNullOrWhiteSpace(username))
				{

					Console.WriteLine("Password:");
					 password = GetConsolePassword();

					if (!string.IsNullOrWhiteSpace(password))
					{
						Console.WriteLine("Authority:");
						 authority = Console.ReadLine();
						var twoMinsIntervalinMilliSec = 120000;
						var thirySecondInterval = 30000;
						myTimer = new System.Timers.Timer(twoMinsIntervalinMilliSec);
						myTimer.Elapsed += OnTimedEvent;
						myTimer.AutoReset = true;
						myTimer.Enabled = true;
						Console.WriteLine("\nPress Enter key to stop...\n");
						Console.ReadLine();
						myTimer.Stop();
						myTimer.Dispose();
						Console.WriteLine("Monitoring services stop...");
						System.Console.ReadKey();
						if (Console.KeyAvailable)
						{
							Environment.Exit(0);
						}

					}




				}
			}

		}

		private static void OnTimedEvent( object sender, ElapsedEventArgs e )
		{
			CheckServerApplicatiosServicesStatus(username, password, authority);
		}

		private static void CheckServerApplicatiosServicesStatus( string username, string password, string authority )
		{
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
