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
		public int SiteNumber { get; set; }
		public int MaxOccupancy { get; set; }
		public string Accessiblity { get; set; }
		public string MaxRevLength { get; set; }
		public string Utilities { get; set; }
		public Campground CampGroundId { get; set; }

	}
}
