using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domian.Entities;
using Infrastructure.Data;
using Services.Interfaces;

namespace ServiceMonitoring
{
	internal class FileAccess
	{
		private readonly ITextRepository textRepository;
		private readonly ITextService textService;

		public FileAccess(ITextRepository textRepository,ITextService textService )
		{
			if (textRepository == null)
			{
				throw new ArgumentNullException(nameof(textRepository));
			}

			if (textService == null)
			{
				throw new ArgumentNullException(nameof(textService));
			}

			this.textRepository = textRepository;
			this.textService = textService;
		}

		public bool Save( string filepath )
		{
			return textService.Save(textRepository, filepath);

		}

		public List<MyText> Get( string filePath )
		{
			return textService.Get(filePath);
			
		}
	}
}
