using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domian.Entities;

namespace Services.Interfaces
{
	public interface ITextService
	{
		/// <summary>
		/// This method will save the repository on the speicified loction
		/// </summary>
		/// <param name="textRepository"> The Text repository to save</param>
		/// <param name="filepath">The fully qualidied path</param>
		bool Save( ITextRepository textRepository , string filepath);


		/// <summary>
		/// Retrive repository data from file path
		/// </summary>
		/// <param name="filepath">The fully qualidied path</param>
		/// <returns>Text Repository</returns>
		List<MyText> Get( string filepath );

	}
}
