//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Interfaces;
//using Domian.Entities;
//using Infrastructure.Data;
//using Services;
//using Services.Interfaces;
//using static System.Net.Mime.MediaTypeNames;

//namespace ServiceMonitoring
//{
//	class TextRepositorySaveRetrive
//	{
//		static ITextService textService = new TextService();
//		static ITextRepository textRepository;
//		static void Main(string[] args)
//		{
//			Console.WriteLine("Press enter key to start.....");
//			var keyinfo = Console.ReadKey();
//			if (keyinfo.Key.Equals(ConsoleKey.Enter))
//				{

//				Console.WriteLine("Give number of text classes you want to create");
//				int numberOfTexts;
//				int.TryParse(Console.ReadLine(), out numberOfTexts);
//				if (numberOfTexts == 0)
//				{
//					Console.WriteLine("Should be more than 0....press any key to exit");
//					System.Console.ReadKey();
//				}
//				else
//				{
//					var listOfTexts = CreateTexts(numberOfTexts);
//					textRepository = new TextRepository(listOfTexts);
//					Console.WriteLine("Give path to save the file....full qulified path with file name");
//					var filePath = Console.ReadLine();
//					var fileAccess = new FileAccess(textRepository, textService);
//					fileAccess.Save(filePath);

//					//Now all the name will show line by line

//					var texts = fileAccess.Get(filePath);
//					foreach (var text in texts )
//					{
//						// Use a tab to indent each line of the file.
//						Console.WriteLine("\t" + text.Name);
//					}

//					Console.WriteLine("Press any key to exit");
//					System.Console.ReadKey();
//					//if (Console.KeyAvailable)
//					//{
//					//	Environment.Exit(0);
//					//}
//				}

//		}
//	}

//		private static List<MyText> CreateTexts( int numberOfTexts )
//		{
//			List<MyText> listOfTexts = new List<MyText>();
//			for (int i = 0; i < numberOfTexts; i++)
//			{
//				Console.WriteLine($"Give name for {i+1} no text class..");
//				var text = new MyText { Name = Console.ReadLine() };
//				listOfTexts.Add(text);
//			}
//			return listOfTexts;
//		}
//		}
//	}
