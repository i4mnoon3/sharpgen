using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SharpGenerator.Models;

namespace SharpGenerator.Repositories.Sql
{
    public class SqlTableRepository : BaseSqlRepository, ITableRepository
    {
        public SqlTableRepository(SqlConnection connection) : base(connection)
        {
        }
        
        public Table Read(string tableName, string databaseName)
        {
            Table table = null;
            string query = @"
select table_name
from information_schema.tables
where table_catalog = @table_catalog
and table_name = @table_name
and table_name != 'dtproperties'";
            using (var rs = ExecuteReader(query, new SqlParameter("@table_name", tableName), new SqlParameter("@table_catalog", databaseName))) {
                if (rs.Read()) {
                    table = new Table(GetString(rs, 0));
                }
            }
            return table;
        }
        
        public List<Table> FindByDatabase(string databaseName)
        {
            var tables = new List<Table>();
            string query = @"
select table_name
from information_schema.tables
where table_catalog = @table_catalog
and table_name != 'dtproperties'
and table_name != 'sysdiagrams'";
            using (var rs = ExecuteReader(query, new SqlParameter("@table_catalog", databaseName))) {
                while (rs.Read()) {
                    tables.Add(new Table(GetString(rs, 0)));
                }
            }
            return tables;
        }
    }
}
