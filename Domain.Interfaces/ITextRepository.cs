using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Entities;

namespace Domain.Interfaces
{
	public interface ITextRepository
	{
		/// <summary>
		/// This method will rename the Name property of the object
		/// </summary>
		/// <param name="text"></param>
		///  <param name="name">New Name</param>
		void Rename( MyText text, string name );
		
		/// <summary>
		///  This method will add the object to the repository
		/// </summary>
		/// <param name="text"></param>
		void Add( MyText text );

		/// <summary>
		/// This method will delete this object from repository
		/// </summary>
		/// <param name="text">Text objet</param>
		void Remove( MyText text );

		/// <summary>
		/// Return list of MyText object
		/// </summary>
		/// <returns>list of MyText object</returns>
		List<MyText> Get();

		
	}
}
