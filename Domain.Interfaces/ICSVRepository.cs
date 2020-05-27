using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
	public interface ICSVRepository
	{
		bool Save(Dictionary<string,List<string>> data);
		Dictionary<string, List<string>> retrive();

	}
}
