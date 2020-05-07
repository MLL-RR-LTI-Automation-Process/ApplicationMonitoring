using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domian.Entities;
using Services.Interfaces;

namespace Services
{
	public class TextService : ITextService
	{
		public List<MyText> Get( string filepath )
		{
			string[] lines = System.IO.File.ReadAllLines(filepath);
			List<MyText> texts = new List<MyText>();
			foreach (string line in lines)
			{
				var myText = new MyText { Name = line };
				texts.Add(myText);
			}
			return texts;
		}

		public bool Save( ITextRepository textRepository, string filepath )
		{
			try
			{
				using (System.IO.StreamWriter file =
			 new System.IO.StreamWriter(filepath))
				{
					foreach (var myText in textRepository.Get())
					{
						file.WriteLine(myText.Name);
					}
				}
				return true;
			}
			catch (Exception)
			{

				return false;
			}
			
		}
	}
}
