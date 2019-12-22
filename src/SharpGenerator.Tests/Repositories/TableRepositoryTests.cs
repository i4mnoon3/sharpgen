using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using SharpGenerator.Repositories.Sql;

namespace SharpGenerator.Tests.Repositories
{
    [TestFixture]
    public class TableRepositoryTests
    {
        string connectionString = ConfigurationManager.AppSettings["healthWatchConnection"];
        SqlConnection connection;
        
        [SetUpAttribute]
        public void Setup()
        {
            connection = new SqlConnection(connectionString);
        }
        
        [Test]
        public void TestFindByDatabase()
        {
            
            var tables = new SqlTableRepository(connection).FindByDatabase("healthWatch");
            foreach (var t in tables) {
                Console.WriteLine(t.Name);
            }
        }
        
        [Test]
        public void TestRead()
        {
            string connectionString = ConfigurationManager.AppSettings["healthWatchConnection"];
            var table = new SqlTableRepository(connection).Read("Wise", "healthWatch");
            Console.WriteLine(table.Name);
        }
    }
}
