using System;

	class ParkSystemCLI
	{
		

		public ParkSystem currentPs;
		const string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground_db;Integrated Security=True";
		public string userInput;


	public ParkSystemCLI(ParkSystem ps)
		{
			currentPs = ps;
		}


		private void PrintHeader()
		
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

			GetNames();
		}


		private void MainMenu(List<string> parks)
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Welcome to the National Parks Reservation System.");
			Console.WriteLine("Please choose one of the parks listed below.");

			for (int i = 0; i < parks.Count; i++)
			{
				Console.WriteLine(i + " - " + parks[i]);
			}
			
			Console.WriteLine($"{parks.Count + 1} - Quit");

			int userInput = Console.Readline();

			if (userInput == parks.Count + 1)
			{
				return;
			}
			
			string parkName = parks[userInput];

			Dictionary parkDictionary = new Dictionary(ParksSqlDAL.GetParkData(parkName));

			Console.WriteLine("Park Information Screen");
			Console.WriteLine($"park[1] {p.name}Key = {0}, Value = {1}", kvp.Key, kvp.Value});



		Console.WriteLine(" 1 - View all campgrounds");
			Console.WriteLine(" 2 - Search for Reservation.");
			Console.WriteLine(" 3 - Return to Previous Screen");
		}
		
	
		

		private void ReservationMenu()
		{
			Console.WriteLine(" 1 - Search for available reservations");
			Console.WriteLine(" 2 - Return to the previous screen");
			
		}

		



		public void RunMenu()
		{
			while (true)
			{
				string userInput = Console.ReadLine().ToUpper();
				if (userInput == ViewParks)
				{
					DisplayAllParks();
					Console.ReadKey();

				}
				else if (userInput == ReservationsMenu)
				{
					DisplayReservationsMenu();
					Console.ReadKey();
				}
				else
				{
					ExecuteOtherMenuOptions(userInput);

				}
			}
		}

		private void ExecuteOtherMenuOptions(string userInput)
		{
			if (userInput == PreviousMenu)
			{
				Console.Clear();
				RunMenu();

			}
			else if (userInput == Quit)
			{
				return;
			}

		}

		private void DisplayReservationsMenu()
		{
			throw new NotImplementedException();
		}

		private void DisplayAllParks()
		{
			string userInput = Console.ReadLine().ToUpper();
			try
			{
				//if(currentPs)
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}


		private void GetParkNames()
		{
			ParksSqlDAL parkDal = new ParksSqlDAL(connectionString);

			IList<string> parks = parkDal.GetParkNames();

			Console.WriteLine();
			
			for (int i = 0; i < parks.Count; i++)
			{
				Console.WriteLine(i + " - " + parks[i]);
			}
		}



	/// <summary>
	/// ////////////////////////
	/// </summary>

	public class WorldGeographyCLI
	{
		
		

		public void RunCLI()
		{
			PrintHeader();
			PrintMenu();

			while (true)
			{
				string command = Console.ReadLine();

				Console.Clear();

				switch (command.ToLower())
				{
					case Command_GetAllContinentNames:
						GetContinentNames();
						break;

					case Command_CountriesInNorthAmerica:
						GetCountriesInNorthAmerica();
						break;

					case Command_CitiesByCountryCode:
						GetCitiesByCountryCode();
						break;

					case Command_LanguagesByCountryCode:
						GetLanguagesForCountry();
						break;

					case Command_AddNewLanguage:
						AddNewLanguage();
						break;

					case Command_RemoveLanguage:
						RemoveLanguage();
						break;

					case Command_Quit:
						Console.WriteLine("Thank you for using the world geography cli app");
						return;

					default:
						Console.WriteLine("The command provided was not a valid command, please try again.");
						break;
				}

				PrintMenu();
			}
		}



		private void PrintHeader()
		{
			Console.WriteLine(@" _    _  _____ ______  _     ______     ______   ___   _____   ___  ______   ___   _____  _____ ");
			Console.WriteLine(@"| |  | ||  _  || ___ \| |    |  _  \    |  _  \ / _ \ |_   _| / _ \ | ___ \ / _ \ /  ___||  ___|");
			Console.WriteLine(@"| |  | || | | || |_/ /| |    | | | |    | | | |/ /_\ \  | |  / /_\ \| |_/ // /_\ \\ `--. | |__  ");
			Console.WriteLine(@"| |/\| || | | ||    / | |    | | | |    | | | ||  _  |  | |  |  _  || ___ \|  _  | `--. \|  __| ");
			Console.WriteLine(@"\  /\  /\ \_/ /| |\ \ | |____| |/ /     | |/ / | | | |  | |  | | | || |_/ /| | | |/\__/ /| |___ ");
			Console.WriteLine(@" \/  \/  \___/ \_| \_|\_____/|___/      |___/  \_| |_/  \_/  \_| |_/\____/ \_| |_/\____/ \____/ ");
		}



		private void PrintMenu()
		{
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Main-Menu Type in a command");
			Console.WriteLine(" 1 - Get all of the continent names");
			Console.WriteLine(" 2 - Get a list of the countries in North America");
			Console.WriteLine(" 3 - Get a list of the cities by country code");
			Console.WriteLine(" 4 - Get a list of the languages by country code");
			Console.WriteLine(" 5 - Add a new language");
			Console.WriteLine(" 6 - Remove language");
			Console.WriteLine(" Q - Quit");
		}




		private void GetContinentNames()
		{
			CountrySqlDAL countryDal = new CountrySqlDAL(DatabaseConnectionString);

			IList<string> continents = countryDal.GetContinentNames();

			Console.WriteLine();
			Console.WriteLine("Printing all of the continents");

			for (int index = 0; index < continents.Count; index++)
			{
				Console.WriteLine(index + " - " + continents[index]);
			}
		}



		private void GetCountriesInNorthAmerica()
		{
			CountrySqlDAL countryDal = new CountrySqlDAL(DatabaseConnectionString);

			IList<Country> northAmericanCountries = countryDal.GetCountriesInNorthAmerica();

			Console.WriteLine();
			Console.WriteLine("All North American Countries");

			foreach (var country in northAmericanCountries)
			{
				Console.WriteLine(country);
			}
		}



		private void GetCitiesByCountryCode()
		{
			string countryCode = CLIHelper.GetString("Enter the country code that you want to retrieve:");

			CitySqlDAL dal = new CitySqlDAL(DatabaseConnectionString);
			IList<City> cities = dal.GetCitiesByCountryCode(countryCode);

			Console.WriteLine();
			Console.WriteLine($"Printing {cities.Count} cities for {countryCode}");

			foreach (var city in cities)
			{
				Console.WriteLine(city);
			}
		}

		private void AddNewLanguage()
		{
			string countryCode = CLIHelper.GetString("Enter the country code the language is for:");
			bool officialOnly = CLIHelper.GetBool("Is it official only? True/False ");
			int percentage = CLIHelper.GetInteger("What percentage is it spoken by?");
			string name = CLIHelper.GetString("What is the name of the lanaguage?");

			Language lang = new Language
			{
				CountryCode = countryCode,
				IsOfficial = officialOnly,
				Percentage = percentage,
				Name = name
			};

			LanguageSqlDAL languageDal = new LanguageSqlDAL(DatabaseConnectionString);
			int result = languageDal.AddNewLanguage(lang);

			if (result > 0)
			{
				Console.WriteLine("Success!");
			}
			else
			{
				Console.WriteLine("The new language was not inserted");
			}
		}

		private void RemoveLanguage()
		{
			throw new NotImplementedException();
		}

		private void GetLanguagesForCountry()
		{
			string countryCode = CLIHelper.GetString("Enter the country code you want to retrieve:");
			bool officialOnly = CLIHelper.GetBool("Retrieve official languages only? True/False ");

			LanguageSqlDAL languageDal = new LanguageSqlDAL(DatabaseConnectionString);
			IList<Language> languages = languageDal.GetLanguages(countryCode, officialOnly);

			Console.WriteLine();
			Console.WriteLine($"Printing {languages.Count} languages for {countryCode}");

			foreach (var language in languages)
			{
				Console.WriteLine(language);
			}
		}




	}




}
