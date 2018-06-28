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
		public int Open { get; set; }
		public int Close { get; set; }
		public decimal Fee { get; set; }
		public Park ParkId { get; set; }

	}
}
