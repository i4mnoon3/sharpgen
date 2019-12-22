using System;
using System.Data;
using System.Data.SqlClient;

namespace SharpGenerator.Repositories.Sql
{
    public class BaseSqlRepository
    {
        SqlConnection connection;
        
        public BaseSqlRepository(SqlConnection connection)
        {
            this.connection = connection;
        }
        
        protected string GetString(SqlDataReader rs, int index)
        {
            return GetString(rs, index, "");
        }
        
        string GetString(SqlDataReader rs, int index, string @default)
        {
            return !rs.IsDBNull(index) ? rs.GetString(index) : @default;
        }
        
        void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed) {
                connection.Open();
            }
        }
        
        void CloseConnection()
        {
            if (connection.State == ConnectionState.Open) {
                connection.Close();
            }
        }
        
        public SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
        {
            OpenConnection();
            var cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }
}
