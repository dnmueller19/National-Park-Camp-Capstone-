using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class CampSite
	{
		public int Id { get; set; }
		public int MaxOccupancy { get; set; }
		public bool Accessiblity { get; set; }
		public int MaxRevLength { get; set; }
		public string Utilities { get; set; }
		public int CampGroundId { get; set; }

	}
}
