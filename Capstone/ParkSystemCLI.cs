using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Capstone;
using Capstone.DAL;
using Capstone.Models;

class ParkSystemCLI
{

	const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";
	public int userInput;
	public string globalParkName;
	bool reservation = false;

	public void RunCLI()
	{
		MainMenu();
	}

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

		//gives us a new park using the get GetParkNames Method
		IList<string> parks = GetParkNames();

		int parkCount = 1;

		//loops through the park list
		//gives each of the parks a number 
		foreach (string p in parks)
		{
			Console.WriteLine($"{parkCount} - {p}");
			parkCount++;
		}

		Console.WriteLine($"{parks.Count + 1} - Quit");
		Console.WriteLine();
		int userInput = 0;

		//prompt user to enter teh park they want to go to 
		//if they choose the quit number(n+1) it wil quit the program 
		//if they are a jackass and enter in a huge number or a negative number 
		//then it will keep promptinf them to enter in a real number
		do
		{
			userInput = (CLIHelper.GetInteger("Please Choose a Park Number: "));

		} while (userInput <= 0 || userInput > parks.Count + 1);

		if (userInput == parks.Count + 1)
		{
			return;
		}

		string parkName = parks[userInput - 1].ToString();

		ParksSqlDAL Parks = new ParksSqlDAL(connectionString);

		//creates a new dictionary with the parks data in it
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
		do {
			userInput = CLIHelper.GetInteger("Please Select a Command: ");

			if (userInput == 1)
			{
				//create a new campground
				//puts it in a dictionary 
				//brings up the ground menu
				CampgroundSqlDAL Campgrounds = new CampgroundSqlDAL(connectionString);
				Dictionary<int, Campground> campGroundDictionary = Campgrounds.GetCampground(parkDictionary[1].Id);
				CampGroundMenu(campGroundDictionary);
			}
			else if (userInput == 2)
			{

			}
			else if (userInput == 3)
			{
				Console.Clear();
				MainMenu();
			}
			else
			{
				Console.WriteLine("Please make a valid choice");
			}
		} while (userInput != 1 || userInput != 2 || userInput != 3);

		
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
				Console.Write($"#{count,-5}");
				Console.Write("{1, -30}", kvp.Key, kvp.Value.Name);
				Console.Write("{1, -10}", kvp.Key,
					CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Value.Open));
				Console.Write("{1, -15}", kvp.Key,
					CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Value.Close));
				Console.Write("{1, -15}", kvp.Key, kvp.Value.Fee.ToString("C"));
				Console.WriteLine();
				siteIdDictionary.Add(count, kvp.Value.Id);
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

				do
				{
					userInput = CLIHelper.GetInteger("Please Select a Command: ");

					if (userInput == 1)
					{
						//if the user inputs 1 
						// will set reservation varible to true
						//then prompt you to enter the campground of your choice
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
				} while (userInput != 1 || userInput != 2);

			}
			else
			{
				//when the user chooses 1 it starts the reservation menu
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
				//will bring back the menu choices
				Console.Clear();
				reservation = false;
				CampGroundMenu(campGroundDictionary);
				
			}
			else
			{
				campgroundChoice = siteIdDictionary[campgroundChoice];
			}
			Console.Clear();

			//prompts the user for arrival and departure dates
			DateTime arrivalDate = CLIHelper.GetDateTime("Please enter the date you will be arriving. (mm/dd/yyyy): ");
			arrivalDate = arrivalDate;

			DateTime departureDate = CLIHelper.GetDateTime("Please enter the date you will be departing. (mm/dd/yyyy): ");
			departureDate = departureDate;

			ReservationSqlDAL reservations = new ReservationSqlDAL(connectionString);

			//creates new dictionary to get open camp sites
			Dictionary<int, CampSite> reservationDictionary =
				reservations.GetOpenCampSites(campgroundChoice, arrivalDate, departureDate);

			Console.Clear();
			Console.WriteLine($"Results matching your search criteria");
			Console.WriteLine();
			Console.Write("Site No.".PadLeft(5));
			Console.Write("Max Occup.".PadLeft(12));
			Console.Write("Accessible?".PadLeft(12));
			Console.Write("Max RV Length".PadLeft(16));
			Console.Write("Utility".PadLeft(12));
			Console.Write("Total Cost".PadLeft(11));
			Console.WriteLine();

			
			if (reservationDictionary.Count < 1)
			{
				string userTemp = "";
				do
				{
					//if no reservations are avalible then prompts user to choose new dates
					Console.Clear();
					userTemp = CLIHelper.GetString(
						"No sites available for those dates.  Would you like to choose alternate dates? y/n");
					if (userTemp == "y")
					{
						//takes user back to the reservation menu
						Console.Clear();
						CampGroundMenu(campGroundDictionary);
					}
					else if (userTemp == "n")
					{
						Console.Clear();
						MainMenu();
					}
				} while (userTemp != "y" || userTemp != "n");
			}


			foreach (KeyValuePair<int, CampSite> kvp in reservationDictionary)
			{
				Console.Write("{1, -10}", kvp.Key, kvp.Value.SiteNumber);
				Console.Write("{1, -11}", kvp.Key, kvp.Value.MaxOccupancy);
				Console.Write("{1, -14}", kvp.Key, kvp.Value.Accessiblity);
				Console.Write("{1, -18}", kvp.Key, kvp.Value.MaxRevLength);
				Console.Write("{1, -10}", kvp.Key, kvp.Value.Utilities);
				int totalDays = (int)(departureDate - arrivalDate).TotalDays;
				if (totalDays < 1)
				{
					totalDays = 1;
				}
				decimal feeTotal = campGroundDictionary[campgroundChoice].Fee * totalDays;
				Console.Write($"{feeTotal:C}".PadLeft(10));
				Console.WriteLine();
			}

			Console.WriteLine();
			Console.WriteLine();
			int siteChoice = CLIHelper.GetInteger("Which site should be reserved (enter 0 to cancel)? ");
			if (siteChoice == 0)
			{
				CampGroundMenu(campGroundDictionary);
			}
			//prompts user for the name they want the reservation under
			string reservationName = CLIHelper.GetString("What name should the reservation be under? ");
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine($"The reservation has been made and the confirmation id is {reservations.AddReservation(siteChoice, arrivalDate, departureDate, reservationName)})");
			Console.ReadLine();

			string temp = "";
			do
			{
				Console.Clear();
				temp = CLIHelper.GetString(
					"Would you like to make another reservation? y/n");
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
	}

	/// <summary>
	/// Gets the park names
	/// </summary>
	/// <returns></returns>
	private IList<string> GetParkNames()
	{
		ParksSqlDAL parkDal = new ParksSqlDAL(connectionString);

		IList<string> parks = parkDal.GetParkNames();

		return parks;
	}

}
