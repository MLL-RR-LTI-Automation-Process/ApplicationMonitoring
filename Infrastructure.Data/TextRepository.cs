using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domian.Entities;
using Services.Interfaces;

namespace Infrastructure.Data
{
    public class TextRepository	:ITextRepository
    {
		private List<MyText> listOfText;

		public List<MyText> ListOfText {
			get
			{
				return listOfText;
			}
		}

		/// <summary>
		/// This is Text Repository
		/// </summary>
		/// <param name="listOfText">Lists of Text objects</param>
		public TextRepository( List<MyText> listOfText )
		{
			if (listOfText == null)
			{
				throw new ArgumentNullException(nameof(listOfText));
			}

			this.listOfText = listOfText;
		}

		public MyText Text { get; }

		public void Add( MyText text )
		{
			if (text == null)
			{
				throw new ArgumentNullException($"Parameter object can not be null");
			}
			listOfText.Add(text);
		}

		public void Remove( MyText text )
		{
			if (text == null)
			{
				throw new ArgumentNullException($"Parameter object can not be null");
			}

			var txt = listOfText.Find(t => t.Name.Equals(text.Name));
			if (txt == null)
			{														
				return;
			}

			listOfText.Remove(text);
		}

		public void Rename( MyText text , string name)
		{
			Remove(text);
			var txt = new MyText { Name = name };
			Add(txt);
		}

		public List<MyText> Get()
		{
			return listOfText;
		}
	}
}
