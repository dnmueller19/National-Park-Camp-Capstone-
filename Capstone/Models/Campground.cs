using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;


namespace Capstone.Models
{
	public class Campground
	{
		//properties for the campground
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Open { get; set; }
		public DateTime Close { get; set; }
		public decimal Fee { get; set; }
		public int ParkId { get; set; }

	}
}
