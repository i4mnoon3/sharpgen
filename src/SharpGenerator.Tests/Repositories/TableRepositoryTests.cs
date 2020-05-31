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
        string connectionString = ConfigurationManager.AppSettings["testConnection"];
        SqlConnection connection;
        
        [SetUpAttribute]
        public void Setup()
        {
            connection = new SqlConnection(connectionString);
        }
        
        [Test]
        public void TestFindByDatabase()
        {
            
            var tables = new SqlTableRepository(connection).FindByDatabase("test");
            foreach (var t in tables) {
                Console.WriteLine(t.Name);
            }
        }
        
        [Test]
        public void TestRead()
        {
            string connectionString = ConfigurationManager.AppSettings["testConnection"];
            var table = new SqlTableRepository(connection).Read("User", "test");
            Console.WriteLine(table.Name);
        }
    }
}
