using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
	class ParkSystemCLI
	{
		private const string ViewParks = "1";
		private const string ReservationsMenu = "2";
		private const string ViewCampground = "1";
		private const string SearchForReservation = "2";
		private const string SearchForAvailibleReservation = "1";
		private const string PreviousMenu = "P";
		private const string Quit = "Q";
		public ParkSystem currentPs;

		public ParkSystemCLI(ParkSystem ps)
		{
			currentPs = ps;
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
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
	}
}
