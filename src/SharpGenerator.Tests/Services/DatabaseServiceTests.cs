using System;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using SharpGenerator.Repositories;
using SharpGenerator.Repositories.Sql;
using SharpGenerator.Services;
using SharpGenerator.Helpers;

namespace SharpGenerator.Tests.Services
{
    [TestFixture]
    public class DatabaseServiceTests
    {
        string connectionString = ConfigurationManager.AppSettings["healthWatchConnection"];
        SqlConnection connection;
        
        [SetUpAttribute]
        public void Setup()
        {
            connection = new SqlConnection(connectionString);
        }
        
        [Test]
        public void TestGetDatabase()
        {
            
            IDatabaseRepository databaseRepo = new SqlDatabaseRepository(connection);
            ITableRepository tableRepo = new SqlTableRepository(connection);
            IColumnRepository columnRepo = new SqlColumnRepository(connection);
            var s = new DatabaseService(databaseRepo, tableRepo, columnRepo);
            
            var d = s.GetDatabase("healthwatch");
            foreach (var t in d.Tables) {
                Console.WriteLine(t.ToClass().ToRepositoryInterfaceString());
                Console.WriteLine(t.ToSqlRepositoryClassString());
            }
        }
    }
}
