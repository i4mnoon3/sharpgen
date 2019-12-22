using System;
using System.Data.SqlClient;
using SharpGenerator.Helpers;
using SharpGenerator.Models;

namespace SharpGenerator.Repositories.Sql
{
    public class SqlDatabaseRepository : BaseSqlRepository, IDatabaseRepository
    {
        public SqlDatabaseRepository(SqlConnection connection) : base(connection)
        {
        }
        
        public Database Read(string name)
        {
            Database database = new Database(name.ToPascalCase()); // null;
            return database;
        }
    }
}
