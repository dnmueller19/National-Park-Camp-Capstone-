using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Park
	{
		//These are the properties for the Park Class
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public DateTime Established { get; set; }
		public int Area { get; set; }
		public int VisitCount { get; set; }
		public string Description { get; set; }

	}
}
