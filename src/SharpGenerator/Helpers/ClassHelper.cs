using System;
using SharpGenerator.Models;

namespace SharpGenerator.Helpers
{
    public static class ClassHelper
    {
        public static string GetBaseSqlRepositoryClassString(string @namespace)
        {
            string s = string.Format(@"using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace __NAMESPACE__.Repositories.Sql
{{
    public class BaseSqlRepository
    {{
        SqlConnection connection;
        
        public BaseSqlRepository()
        {{
        }}
        
        protected decimal GetDecimal(SqlDataReader rs, int index)
        {{
            return GetDecimal(rs, index, 0);
        }}
        
        protected decimal GetDecimal(SqlDataReader rs, int index, decimal @default)
        {{
            return !rs.IsDBNull(index) ? rs.GetDecimal(index) : @default;
        }}
        
        protected DateTime GetDateTime(SqlDataReader rs, int index)
        {{
            return GetDateTime(rs, index, DateTime.Now);
        }}
        
        protected DateTime GetDateTime(SqlDataReader rs, int index, DateTime @default)
        {{
            return !rs.IsDBNull(index) ? rs.GetDateTime(index) : @default;
        }}
        
        protected int GetInt32(SqlDataReader rs, int index)
        {{
            return GetInt32(rs, index, 0);
        }}
        
        protected int GetInt32(SqlDataReader rs, int index, int @default)
        {{
            return !rs.IsDBNull(index) ? rs.GetInt32(index) : @default;
        }}
        
        protected string GetString(SqlDataReader rs, int index)
        {{
            return GetString(rs, index, """");
        }}
        
        protected string GetString(SqlDataReader rs, int index, string @default)
        {{
            return !rs.IsDBNull(index) ? rs.GetString(index) : @default;
        }}
        
        void OpenConnection()
        {{
            if (connection.State == ConnectionState.Closed) {{
                connection.Open();
            }}
        }}
        
        void CloseConnection()
        {{
            if (connection.State == ConnectionState.Open) {{
                connection.Close();
            }}
        }}
        
        public SqlDataReader ExecuteReader(string query, params SqlParameter[] parameters)
        {{
            OpenConnection();
            var cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }}
        
        public void ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {{
            OpenConnection();
            var cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }}
    }}
}}");
            s = s.Replace("__NAMESPACE__", @namespace);
            return s;
        }
        
        public static string ToSqlRepositoryClassString(this Table t)
        {
            string s = string.Format(@"using System;
using System.Collections.Generic;
using __NAMESPACE__.Models;

namespace __NAMESPACE__.Repositories.Sql
{{
    public class Sql__NAME__Repository : BaseSqlRepository, I__NAME__Repository
    {{
        public Sql__NAME__Repository()
        {{
        }}
        
        public List<__NAME__> FindAll()
        {{
            string query = @""
SELECT __COLS__
FROM __TABLE__"";
            var __VAR__s = new List<__NAME__>();
            using (var rs = ExecuteReader(query)) {{
                while (rs.Read()) {{
                    var __VAR__ = new __NAME__();
__PROP_VALS__
                    __VAR__s.Add(__VAR__);
                }}
            }}
            return __VAR__s;
        }}
        
        public __NAME__ Read(int id)
        {{
            string query = @""
SELECT __COLS__
FROM __TABLE__
WHERE __KEY_AND_VALS__"";
            __NAME__ __VAR__ = null;
            using (var rs = ExecuteReader(query)) {{
                if (rs.Read()) {{
                    __VAR__ = new __NAME__();
__PROP_VALS__
                }}
            }}
            return __VAR__;
        }}
        
        public void Save(__NAME__ __VAR__)
        {{
            string query = @""
INSERT INTO __TABLE__(__COLS__)
VALUES(__COL_VALS__)"";
            ExecuteNonQuery(query);
        }}
        
        public void Update(__NAME__ __VAR__, int id)
        {{
            string query = @""
UPDATE __TABLE__ SET
__COL_AND_VALS__
WHERE __KEY_AND_VALS__"";
            ExecuteNonQuery(query);
        }}
        
        public void Delete(int id)
        {{
            string query = @""
DELETE FROM __TABLE__
WHERE __KEY_AND_VALS__"";
            ExecuteNonQuery(query);
        }}
    }}
}}");
            Class c = t.ToClass();
            s = s.Replace("__NAMESPACE__", c.Namespace);
            s = s.Replace("__NAME__", c.Name);
            s = s.Replace("__VAR__", c.Name.ToCamelCase());
            s = s.Replace("__TABLE__", c.Name);
            string cols = "";
            string colVals = "";
            string propVals = "";
            string colAndVals = "";
            int i = 0;
            foreach (var p in c.Properties) {
                if (i > 0) {
                    propVals +=  Environment.NewLine;
                    colAndVals += ", " + Environment.NewLine;
                }
                cols += p.Name + ", ";
                colVals += "@" + p.Name + ", ";
                propVals += "                    " + c.Name.ToCamelCase() + "." + p.Name + " = " + GetDbType(p.Type, i) + ";";
                colAndVals += "    " + p.Name + " = @" + p.Name;
                i++;
            }
            string keyAndVals = "";
            foreach (var col in t.Keys) {
                keyAndVals += col.Name.ToPascalCase() + " = @" + col.Name.ToPascalCase() + ", ";
            }
            s = s.Replace("__COLS__", cols.Trim().Trim(','));
            s = s.Replace("__COL_VALS__", colVals.Trim().Trim(','));
            s = s.Replace("__PROP_VALS__", propVals);
            s = s.Replace("__COL_AND_VALS__", colAndVals.Trim(','));
            s = s.Replace("__KEY_AND_VALS__", keyAndVals.Trim().Trim(','));
            return s;
        }
        
        static string GetDbType(string type, int index)
        {
            switch (type) {
                case "int": return "GetInt32(rs, " + index + ")";
                case "DateTime": return "GetDateTime(rs, " + index + ")";
                case "double": return "GetDouble(rs, " + index + ")";
                case "float": return "GetFloat(rs, " + index + ")";
                case "decimal": return "GetDecimal(rs, " + index + ")";
                default: return "GetString(rs, " + index + ")";
            }
        }
        
        public static string ToRepositoryInterfaceString(this Class c)
        {
            string s = string.Format(@"using System;
using System.Collections.Generic;
using __NAMESPACE__.Models;

namespace __NAMESPACE__.Repositories
{{
    public interface I__NAME__Repository
    {{
        void Save(__NAME__ __VAR__);
        void Update(__NAME__ __VAR__, int id);
        __NAME__ Read(int id);
        List<__NAME__> FindAll();
    }}
}}");
            s = s.Replace("__NAMESPACE__", c.Namespace);
            s = s.Replace("__NAME__", c.Name);
            s = s.Replace("__VAR__", c.Name.ToCamelCase());
            string properties = "";
            foreach (var p in c.Properties) {
                properties += "\t\t" + p.ToString() + Environment.NewLine;
            }
            s = s.Replace("__PROPERTIES__", properties);
            return s;
        }
    }
}
