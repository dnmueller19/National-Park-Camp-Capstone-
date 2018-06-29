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
		private string connectionString;

		public const string searchCampgroundDates = "SELECT min(open_from_mm), max(open_to_mm) FROM campground";

		public const string viewCampground = "SELECT * FROM campground WHERE park_id = @parkid;";


		//set constructor
		public CampgroundSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		/// <summary>
		/// Gets the campgrounds and places them in a dictionary
		/// DATATYPE: is a dictionary
		/// </summary>
		/// <param name="parkName"></param>
		/// <returns></returns>
		public Dictionary<int, Campground> GetCampground(int parkid)
		{
			Dictionary<int, Campground> campground = new Dictionary<int, Campground>();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand command = new SqlCommand(viewCampground, conn);
					command.Parameters.AddWithValue("@parkid", parkid);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						//create new camp ground 
						Campground cg = new Campground();

						//set campground properites (reader[])
						cg.Id = Convert.ToInt32(reader["campground_id"]);
						cg.Name = Convert.ToString(reader["name"]);
						cg.Open = Convert.ToInt32(reader["open_from_mm"]);
						cg.Close = Convert.ToInt32(reader["open_to_mm"]);
						cg.Fee = Convert.ToDecimal(reader["daily_fee"]);

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
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand command = new SqlCommand(searchCampgroundDates, conn);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						//populate the list 
						dateTimes.Add(Convert.ToDateTime(reader["open_from_mm"]));
						dateTimes.Add(Convert.ToDateTime(reader["open_to_mm"]));

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
