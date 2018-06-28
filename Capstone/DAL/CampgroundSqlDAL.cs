using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.DAL
{
	public class CampgroundSqlDAL
	{
		//declare object
		private string connectionSting;

		public const string searchCampgroundDates = "SELECT min(opn_frm_mm), max(opn_to_mm) FROM cmpgrnd_tbl";

		public const string viewCampground = "SELECT * FROM cmpgrnd_tbl WHERE park_id"; //will need to change the park id to somehting else


		//set constructor
		public CampgroundSqlDAL(string dbConnectionString)
		{
			connectionSting = dbConnectionString;
		}

		/// <summary>
		/// Gets the campgrounds and places them in a dictionary
		/// DATATYPE: is a dictionary
		/// </summary>
		/// <param name="parkName"></param>
		/// <returns></returns>
		public Dictionary<int, Campground> GetCampground(string parkName)
		{
			Dictionary<int, Campground> campground = new Dictionary<int, Campground>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionSting))
				{
					conn.Open();
					SqlCommand command = new SqlCommand(viewCampground, conn);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						//create new camp ground 
						Campground cg = new Campground();

						//set campground properites (reader[])
						cg.Id = Convert.ToInt32(reader["cmpgrnd_id"]);
						cg.Name = Convert.ToString(reader["name"]);
						cg.Open = Convert.ToDateTime(reader["opn_frm_mm"]);
						cg.Close = Convert.ToDateTime(reader["opn_to_mm"]);
						cg.Fee = Convert.ToDecimal(reader["daily_fee"]);
						cg.ParkId = Convert.ToInt32(reader["park_id"]);

						campground.Add(cg.Id, cg);
					}
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return campground;

		}

		/// <summary>
		/// Will bring back the dates that the campground is open
		/// </summary>
		/// <returns></returns>
		public List<DateTime> GetCampgroundDates()
		{
			List<DateTime> dateTimes = new List<DateTime>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionSting))
				{
					conn.Open();

					SqlCommand command = new SqlCommand(searchCampgroundDates, conn);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						//populate the list 
						dateTimes.Add(Convert.ToDateTime(reader["opn_frm_mm"]));
						dateTimes.Add(Convert.ToDateTime(reader["opn_to_mm"]);

					}

				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return dateTimes;

		}
	}
}
