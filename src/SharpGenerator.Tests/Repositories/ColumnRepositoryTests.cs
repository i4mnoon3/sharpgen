using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using SharpGenerator.Repositories.Sql;

namespace SharpGenerator.Tests.Repositories
{
    [TestFixture]
    public class ColumnRepositoryTests
    {
        string connectionString = ConfigurationManager.AppSettings["testConnection"];
        SqlConnection connection;
        
        [SetUpAttribute]
        public void Setup()
        {
            connection = new SqlConnection(connectionString);
        }
        
        [Test]
        public void TestFindByTable()
        {
            var columns = new SqlColumnRepository(connection).FindByTable("User", "test");
            foreach (var c in columns) {
                Console.WriteLine(c.Name);
            }
        }
    }
}
