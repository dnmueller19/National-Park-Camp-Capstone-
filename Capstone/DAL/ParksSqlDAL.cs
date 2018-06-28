using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;
using System.Data.SqlClient;

namespace Capstone.DAL
{
	public class ParksSqlDAL
	{
		//set object
		public const string viewPark = "SELECT * FROM park_tbl ORDER BY name";

		private string connectionString;

		//public string ConnectionString
		//{
		//	get { return connectionString; }
		//	set { connectionString = value; }
		//}

		//set constructor
		public ParksSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		/// <summary>
		/// will getg he park data
		/// </summary>
		/// <param name="parkId"></param>
		/// <returns></returns>
		public Dictionary<int, Park> GetParkData(int parkId)
		{
			Dictionary<int, Park> parks = new Dictionary<int, Park>();
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand(viewPark, conn);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					//create new park
					Park p = new Park();

					//set the properties for the parks
					//need to convert to int or string 
						//set them to the reader using []
					p.Id = Convert.ToInt32(reader["park_id"]);
					p.Name = Convert.ToString(reader["name"]);
					p.Location = Convert.ToString(reader["location"]);
					p.Established = Convert.ToDateTime(reader["establishedDate"]);
					p.Area = Convert.ToInt32(reader["area"]);
					p.VisitCount = Convert.ToInt32(reader["annualVisitCount"]);
					p.Description = Convert.ToString(reader["description"]);

					//add park and parkID to dictionary
					parks.Add(p.Id, p);
				}
			}
			return parks;
		}
	}
}
