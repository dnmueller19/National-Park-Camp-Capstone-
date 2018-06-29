using System;
using Capstone.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class CampgroundSqlDALTests : CapstoneTests
	{
		[TestMethod]
		public void GetCampgroundTest_WithFakePark()
		{
			CampgroundSqlDAL dal = new CampgroundSqlDAL(ConnectionString);


			// Act
			var campgrounds = dal.GetCampground(1);

			// Assert
			Assert.AreEqual(1, campgrounds.Count);


		}
	}
}
