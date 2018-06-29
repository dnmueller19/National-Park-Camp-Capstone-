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
	public class ReservationSqlDAL
	{
		//declare object
		private string connectionString;

		public const string searchCampGroundReservations = "SELECT min(open_from_mm), max(open_to_mm) FROM campground WHERE @startDate > open_from_mm AND @endDate < open_to_mm";

		public const string searchAllReservation = "SELECT min(open_from_mm), max(open_to_mm) FROM campground WHERE @startDate > open_from_mm AND @endDate < open_to_mm";

		public const string viewSites = "SELECT * FROM site JOIN campground ON site.campground_id = campground.campground_id WHERE campground.campground_id = @cgId AND open_from_mm <= @from_date_month AND open_to_mm >= @to_date_month AND open_from_mm <= @from_date_month AND open_to_mm >= @to_date_month AND NOT EXISTS ( SELECT reservation.site_id FROM reservation JOIN site ON site.site_id = reservation.site_id JOIN campground ON site.campground_id = campground.campground_id WHERE campground.campground_id = @cgId AND (reservation.from_date BETWEEN @from_date AND @to_date) AND (reservation.to_date BETWEEN @from_date AND @to_date));";

		public const string SqlAddReservation = "INSERT INTO reservation VALUES (@siteId, @name, @from_date, @to_date,@create_date);";

		//set constructor
		public ReservationSqlDAL(string dbConnectionString)
		{
			connectionString = dbConnectionString;
		}


		/// <summary>
		/// Gets the campgrounds and places them in a dictionary
		/// DATATYPE: is a dictionary
		/// </summary>
		/// <param name="parkName"></param>
		/// <returns></returns>
		public Dictionary<int, CampSite> GetOpenCampSites(int campgroundId, DateTime arrivalDate, DateTime depatureDate)
		{
			Dictionary<int, CampSite> campSite = new Dictionary<int, CampSite>();

			int intArrivalDate = arrivalDate.Month;
			int intDepartureDate = depatureDate.Month;

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand command = new SqlCommand(viewSites, conn);
					command.Parameters.AddWithValue("@cgId", campgroundId);
					command.Parameters.AddWithValue("@from_date_month", intArrivalDate);
					command.Parameters.AddWithValue("@to_date_month", intDepartureDate);
					command.Parameters.AddWithValue("@from_date", arrivalDate);
					command.Parameters.AddWithValue("@to_date", depatureDate);
					SqlDataReader reader = command.ExecuteReader();
					
					while (reader.Read())
					{
						//create new camp ground 
						CampSite sites = new CampSite();
						//set campground properites (reader[])
						sites.Id = Convert.ToInt32(reader["site_id"]);
						sites.SiteNumber = Convert.ToInt32(reader["site_number"]);
						sites.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
						sites.Accessiblity = Convert.ToString(reader["accessible"]);
						if (sites.Accessiblity == "true")
						{
							sites.Accessiblity = "Yes";
						}
						else
						{
							sites.Accessiblity = "No";
						}
						
						sites.MaxRevLength = Convert.ToString(reader["max_rv_length"]);
						if (sites.MaxRevLength == null || sites.MaxRevLength == "0")
						{
							sites.MaxRevLength = "N/A";
						}
						
						sites.Utilities = Convert.ToString(reader["utilities"]);
						if (sites.Utilities == "false")
						{
							sites.Utilities = "N/A";
						}
						else
						{
							sites.Utilities = "Yes";
						}

						if (!campSite.ContainsKey(sites.Id))
						{
							campSite.Add(sites.Id, sites);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return campSite;

		}

		/// <summary>
		/// Search for reservations for a single camp ground
		/// </summary>
		/// <param name="siteId"></param>
		/// <returns></returns>
		public Dictionary<int, Reservation> SearchReservations_For_SingleGround(int siteId)
		{
			Dictionary<int, Reservation> reservations = new Dictionary<int, Reservation>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand command = new SqlCommand(searchCampGroundReservations, conn);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						Reservation newReservation = new Reservation();
						newReservation.Id = Convert.ToInt32(reader["reservation_id"]);
						newReservation.CampSiteId = Convert.ToInt32(reader["site_id"]);
						newReservation.StartDate = Convert.ToDateTime(reader["from_date"]);
						newReservation.EndDate = Convert.ToDateTime(reader["to_date"]);
						newReservation.Name = Convert.ToString(reader["name"]);
						newReservation.CreateDate = Convert.ToDateTime(reader["create_date"]);

						reservations.Add(newReservation.Id, newReservation);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return reservations;
		}


		//search for avaible reservations for all campsites
		public Dictionary<int, Reservation> SearchReservations_For_AllCampgrounds(int siteId)
		{
			Dictionary<int, Reservation> reservations = new Dictionary<int, Reservation>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand command = new SqlCommand(searchAllReservation, conn);
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						Reservation newReservation = new Reservation();
						newReservation.Id = Convert.ToInt32(reader["reservation_id"]);
						newReservation.CampSiteId = Convert.ToInt32(reader["site_id"]);
						newReservation.StartDate = Convert.ToDateTime(reader["from_date"]);
						newReservation.EndDate = Convert.ToDateTime(reader["to_date"]);
						newReservation.Name = Convert.ToString(reader["name"]);
						newReservation.CreateDate = Convert.ToDateTime(reader["create_date"]);

						reservations.Add(newReservation.Id, newReservation);
					}
				}
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

			return reservations;
		}

		//add a new reservation

		public void AddReservation(int siteId, string arrivalDate, string depatureDate, string reservationName)
		{
			Dictionary<int, Reservation> addReservation = new Dictionary<int, Reservation>();
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					//(@siteId, @name, @from_date, @to_date, @create_date)
					conn.Open();
					SqlCommand command = new SqlCommand(SqlAddReservation, conn);
					command.Parameters.AddWithValue("@siteId", siteId);
					command.Parameters.AddWithValue("@name", reservationName);
					command.Parameters.AddWithValue("@from_date", arrivalDate);
					command.Parameters.AddWithValue("@to_date", depatureDate);
					command.Parameters.AddWithValue("@create_date", DateTime.UtcNow);
					command.ExecuteNonQuery();
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			
		}
	} 
		

	


		
}
