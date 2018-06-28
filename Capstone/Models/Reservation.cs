using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Reservation
	{
		public int Id { get; set; }
		public int CampSiteId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Name { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
