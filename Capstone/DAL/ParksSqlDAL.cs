using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
	public class ParksSqlDAL
	{
		//set object
		public const string viewPark = "SELECT * FROM park ORDER BY name";
		public const string viewParkData = @"SELECT * FROM park WHERE name = @parkName;";
		private readonly string connectionString;
		

		
		public ParksSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}

		/// <summary>
		/// will getg he park data
		/// </summary>
		/// <param name="parkId"></param>
		/// <returns></returns>
		public Dictionary<int, Park> GetParkData(string parkName)
		{
			Dictionary<int, Park> parks = new Dictionary<int, Park>();
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				SqlCommand command = new SqlCommand(viewParkData, conn);
				command.Parameters.AddWithValue("@parkName", parkName);
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
					p.Established = Convert.ToDateTime(reader["establish_date"]);
					p.Area = Convert.ToInt32(reader["area"]);
					p.VisitCount = Convert.ToInt32(reader["visitors"]);
					p.Description = Convert.ToString(reader["description"]);

					//add park and parkID to dictionary
					parks.Add(1, p);
				}
			}
			return parks;
		}

		public IList<string> GetParkNames()
		{
			List<string> output = new List<string>();

			//Always wrap connection to a database in a try-catch block
			try
			{
				//Create a SqlConnection to our database
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand();
					cmd.CommandText = viewPark;
					cmd.Connection = conn;

					// Execute the query to the database
					SqlDataReader reader = cmd.ExecuteReader();

					// The results come back as a SqlDataReader. Loop through each of the rows
					// and add to the output list
					while (reader.Read())
					{
						// Read in the value from the reader
						// Reference by index or by column_name
						string names = Convert.ToString(reader["name"]);

						// Add the continent to the output list
						output.Add(names);
					}
				}
			}
			catch (SqlException ex)
			{
				// A SQL Exception Occurred. Log and throw to our application!!
				throw;
			}

			// Return the list of continents
			return output;
		}
	}
}
