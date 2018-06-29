using System;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class ParksSqlDALTests : CapstoneTests
	{
		[TestMethod]
		public void GetParkDataTest_WithFakePark()
		{
			ParksSqlDAL dal = new ParksSqlDAL(ConnectionString);


			// Act
			var parks = dal.GetParkData("FAKEASS PARK");

			// Assert
			Assert.AreEqual(1, parks.Count);


		}
	}

}
