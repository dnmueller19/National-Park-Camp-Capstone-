using System;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class ReservationSqlDALTests : CapstoneTests
	{
		[TestMethod]
		public void GetReservationTest_WithFakeReservation()
		{
			ReservationSqlDAL dal = new ReservationSqlDAL(ConnectionString);

			// dummy date temps for arguments
			DateTime temp1 = DateTime.Now;
			DateTime temp2 = DateTime.Now;

			// Act
			var reservations = dal.GetOpenCampSites(1, temp1, temp2);

			// Assert
			Assert.AreEqual(1, reservations.Count);


		}

		[TestMethod]
		public void GetSiteTest_WithFakeSite()
		{
			ReservationSqlDAL dal = new ReservationSqlDAL(ConnectionString);

			// dummy date temps for arguments
			DateTime temp1 = DateTime.Now;
			DateTime temp2 = DateTime.Now;

			// Act
			var sites = dal.GetOpenCampSites(1, temp1, temp2);

			// Assert
			Assert.AreEqual(1, sites.Count);


		}
	}
}
