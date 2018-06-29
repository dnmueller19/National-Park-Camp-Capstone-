using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
	public class CampSiteSqlDAL
	{
		//declare objects
		private string connectionString;
		public const string siteReservationData = "SELECT * FROM site_tbl JOIN campground ON site.campground_id = campground.camground_id WHERE site.campground_id = @campgroundId AND site_id NOT IN (SELECT site_id FROM reservation WHERE (@from_date < from_date AND @to_date > to_date))";
		
		//set constructor
		public CampSiteSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campgroundId"></param>
		/// <param name="arrivalDate"></param>
		/// <param name="depatureDate"></param>
		/// <returns></returns>
		public Dictionary<int, CampSite> GetCampsiteData(string campgroundId, string arrivalDate, string depatureDate)
		{
			Dictionary<int, CampSite> campsite = new Dictionary<int, CampSite>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand command = new SqlCommand(siteReservationData, conn);
					command.Parameters.AddWithValue("@campgroundId", campgroundId);
					command.Parameters.AddWithValue("@from_date", arrivalDate);
					command.Parameters.AddWithValue("@to_date", depatureDate);

					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						CampSite cs = new CampSite();

						cs.Id = Convert.ToInt32(reader["site_id"]);
						cs.CampGroundId = Convert.ToInt32(reader["campground_id"]);
						cs.Accessiblity = Convert.ToBoolean(reader["accessible"]);
						cs.MaxRevLength = Convert.ToInt32(reader["max_rv_length"]);
						cs.Utilities = Convert.ToString(reader["utilities"]);
						cs.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
					}


				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return campsite;
		}


	}
}
