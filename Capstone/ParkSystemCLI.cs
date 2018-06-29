using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Capstone;
using Capstone.DAL;
using Capstone.Models;

class ParkSystemCLI
{


	// public ParkSystem currentPs;
	const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";
	public string userInput;
	public string globalParkName;
	bool reservation = false;
	public void RunCLI()
	{
		MainMenu();
	}

	//public ParkSystemCLI(ParkSystem ps)
	//	{
	//		currentPs = ps;
	//	}


	private void MainMenu()

	{

		Console.WriteLine(@"_________                         ________                                ___");
		Console.WriteLine(@"\_   ___ \_____    _____ ______  /  _____/______  ____  __ __  ____    __| _/");
		Console.WriteLine(@"/    \  \/\__  \  /     \\____ \/   \  __\_  __ \/  _ \|  |  \/    \  / __ | ");
		Console.WriteLine(@"\     \____/ __ \|  Y Y  \  |_> >    \_\  \  | \(  <_> )  |  /   |  \/ /_/ | ");
		Console.WriteLine(@" \______  (____  /__|_|  /   __/ \______  /__|   \____/|____/|___|  /\____ | ");
		Console.WriteLine(@"        \/     \/      \/|__|           \/                        \/      \/ ");
		Console.WriteLine(@"_________                                          ___ .__");
		Console.WriteLine(@"\______  \ ____   ______ ______________  _______ _/  |_|__| ____   ____  ");
		Console.WriteLine(@"|       _// __ \ /  ___// __ \_  __ \  \/ /\__  \\   __\  |/  _ \ /    \ ");
		Console.WriteLine(@"|    |   \  ___/ \___ \\  ___/|  | \/\   /  / __ \|  | |  (  <_> )   |  \");
		Console.WriteLine(@"|____|_  /\___  >____  >\___  >__|    \_/  (____  /__| |__|\____/|___|  /");
		Console.WriteLine(@"       \/     \/     \/     \/                  \/                    \/ ");

		Console.WriteLine();
		Console.WriteLine();
		Console.WriteLine("Welcome to the National Parks Reservation System.");
		Console.WriteLine("Please choose one of the parks listed below.");
		Console.WriteLine();

		IList<string> parks = GetParkNames();

		for (int i = 0; i < parks.Count; i++)
		{
			Console.WriteLine($"{i+1} - " + parks[i]);
		}

		Console.WriteLine($"{parks.Count + 1} - Quit");
		Console.WriteLine();
		int userInput = (CLIHelper.GetInteger("Please Choose a Park Number: ") - 1);

		if (userInput == parks.Count + 1)
		{
			return;
		}

		string parkName = parks[userInput].ToString();

		ParksSqlDAL Parks = new ParksSqlDAL(connectionString);

		Dictionary<int, Park> parkDictionary = Parks.GetParkData(parkName);

		globalParkName = parkDictionary[1].Name;

		Console.Clear();
		Console.WriteLine("Park Information Screen");
		Console.WriteLine();
		Console.WriteLine($"Park Name: {parkDictionary[1].Name}");
		Console.WriteLine($"Location: {parkDictionary[1].Location}");
		Console.WriteLine($"Established: {parkDictionary[1].Established.ToShortDateString()}");
		Console.WriteLine($"Area: {string.Format("{0:n0}", parkDictionary[1].Area)}");
		Console.WriteLine($"Annual Visitors: {string.Format("{0:n0}", parkDictionary[1].VisitCount)}");
		Console.WriteLine();
		Console.WriteLine($"{parkDictionary[1].Description}");
		Console.WriteLine();
		Console.WriteLine();
		Console.WriteLine();
		Console.WriteLine(" 1 - View all campgrounds");
		Console.WriteLine(" 2 - Search for Reservation.");
		Console.WriteLine(" 3 - Return to Previous Screen");
		Console.WriteLine();
		userInput = CLIHelper.GetInteger("Please Select a Command: ");
		
		if (userInput == 1)
		{
			CampgroundSqlDAL Campgrounds = new CampgroundSqlDAL(connectionString);
			Dictionary<int, Campground> campGroundDictionary = Campgrounds.GetCampground(parkDictionary[1].Id);
			CampGroundMenu(campGroundDictionary);
		}
		else if (userInput == 2)
		{

		}
		else if (userInput == 3)
		{
			MainMenu();
		}
		else
		{
			Console.WriteLine("Please make a valid choice");
		}

		void CampGroundMenu(Dictionary<int, Campground> campGroundDictionary)
		{
			int count = 1;
			
			Console.Clear();
			Console.WriteLine($"{globalParkName} Park Campgrounds");
			Console.WriteLine();
			Console.Write("Name".PadLeft(10));
			Console.Write("Open".PadLeft(30));
			Console.Write("Close".PadLeft(11));
			Console.WriteLine("Daily Fee".PadLeft(19));
			Console.WriteLine();
			Dictionary<int, int> siteIdDictionary = new Dictionary<int, int>();
			siteIdDictionary.Add(0, 0);
			foreach (KeyValuePair<int, Campground> kvp in campGroundDictionary)
			{
				Console.Write($"#{count, -5}");
				Console.Write("{1, -30}", kvp.Key, kvp.Value.Name);
				Console.Write("{1, -10}", kvp.Key,
					CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Value.Open));
				Console.Write("{1, -15}", kvp.Key,
					CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Value.Close));
				Console.Write("{1, -15}", kvp.Key, kvp.Value.Fee.ToString("C"));
				Console.WriteLine();
				siteIdDictionary.Add(count,kvp.Value.Id);
				count++;
			}

			if (reservation == false)
			{ 
				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine(" 1 - Search for Available Reservation.");
				Console.WriteLine(" 2 - Return to Previous Screen");
				Console.WriteLine();
				userInput = CLIHelper.GetInteger("Please Select a Command: ");

				if (userInput == 1)
				{
					reservation = true;
					CampGroundMenu(campGroundDictionary);
					
				}
				else if (userInput == 2)
				{
					CampGroundMenu(campGroundDictionary);
				}
				else
				{
					Console.WriteLine("Please make a valid choice");
				}

			}
			else
			{
				ReservationMenu(campGroundDictionary, siteIdDictionary);
			}
		}


		void ReservationMenu(Dictionary<int, Campground> campGroundDictionary, Dictionary<int, int> siteIdDictionary)
		{

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			int campgroundChoice = CLIHelper.GetInteger("Which campground (enter 0 to cancel)?: ");
			if (campgroundChoice == 0)
			{
				return;
			}
			else
			{
				campgroundChoice = siteIdDictionary[campgroundChoice];
			}
			Console.Clear();

			DateTime arrivalDate = CLIHelper.GetDateTime("Please enter the date you will be arriving. (mm/dd/yyyy): ");
			arrivalDate = arrivalDate.AddDays(-1);

			DateTime departureDate = CLIHelper.GetDateTime("Please enter the date you will be departing. (mm/dd/yyyy): ");
			departureDate = departureDate.AddDays(1);

			ReservationSqlDAL reservations = new ReservationSqlDAL(connectionString);

			Dictionary<int, CampSite> reservationDictionary =
				reservations.GetOpenCampSites(campgroundChoice, arrivalDate, departureDate);

			Console.Clear();
			Console.WriteLine($"Results Matching Your Criteria of {arrivalDate} to {departureDate}");
			Console.WriteLine();
			Console.Write("Site No.".PadLeft(5));
			Console.Write("Max Occup.".PadLeft(12));
			Console.Write("Accessible?".PadLeft(12));
			Console.Write("Max RV Length".PadLeft(16));
			Console.Write("Utility".PadLeft(12));
			Console.Write("Cost".PadLeft(11));
			Console.WriteLine();

			if (reservationDictionary.Count < 1)
			{
				string temp = "";
				do
				{
					Console.Clear();
					temp = CLIHelper.GetString(
						"No sites available for those dates.  Would you like to choose alternate dates? y/n");
					if (temp == "y")
					{
						Console.Clear();
						CampGroundMenu(campGroundDictionary);
					}
					else if (temp == "n")
					{
						Console.Clear();
						MainMenu();
					}
				} while (temp != "y" || temp != "n");
			}

			foreach (KeyValuePair<int, CampSite> kvp in reservationDictionary)
			{
				Console.Write("{1, -10}", kvp.Key, kvp.Value.SiteNumber);
				Console.Write("{1, -11}", kvp.Key,kvp.Value.MaxOccupancy);
				Console.Write("{1, -14}", kvp.Key,kvp.Value.Accessiblity);
				Console.Write("{1, -18}", kvp.Key, kvp.Value.MaxRevLength);
				Console.Write("{1, -10}", kvp.Key, kvp.Value.Utilities);
				Console.Write($"{campGroundDictionary[campgroundChoice].Fee:C}".PadLeft(10));
				Console.WriteLine();
			}

			Console.ReadLine();
			CampGroundMenu(campGroundDictionary);

		}


	}




	private IList<string> GetParkNames()
	{
		ParksSqlDAL parkDal = new ParksSqlDAL(connectionString);

		IList<string> parks = parkDal.GetParkNames();

		return parks;
	}

}
