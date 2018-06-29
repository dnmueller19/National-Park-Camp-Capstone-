using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
	[TestClass]
	public class CapstoneTests
	{
		
		public const string ConnectionString =
			@"Data Source=.\SQLEXPRESS;Initial Catalog=Campground;Integrated Security=True";

		private TransactionScope transaction;
		

		[TestInitialize]
		public void SetupData()
		{

			// Begin transaction
			transaction = new TransactionScope();


			// Reads in all of the sql from database.sql
			string sql = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "capstone.sql"));

			// Run this sql against the database
			using (SqlConnection conn = new SqlConnection(ConnectionString))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
			}
		}

		[TestCleanup]
		public void CleanupData()
		{
			// Rollback transaction
			transaction.Dispose();
		}


	}
}
