using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using SharpGenerator.Helpers;
using SharpGenerator.Repositories.Sql;
using SharpGenerator.Services;

namespace SharpGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Run(args);
        }
        
        public void Run(string[] args)
        {
            if (args.Length == 1) {
                string connectionString = args[0];
                
                var connection = new SqlConnection(connectionString);
                
                var databaseRepository = new SqlDatabaseRepository(connection);
                var tableRepository = new SqlTableRepository(connection);
                var columnRepository = new SqlColumnRepository(connection);
                
                var service = new DatabaseService(databaseRepository, tableRepository, columnRepository);
                var d = service.GetDatabase(connection.Database);
                
                Directory.CreateDirectory(Path.Combine("Models"));
                Directory.CreateDirectory(Path.Combine("Repositories"));
                Directory.CreateDirectory(Path.Combine(Path.Combine("Repositories", "Sql")));
                
                SaveFile(Path.Combine(Path.Combine("Repositories", "Sql"), "BaseSqlRepository.cs"), ClassHelper.GetBaseSqlRepositoryClassString(connection.Database.ToPascalCase()));
                
                foreach (var t in d.Tables) {
                    var c = t.ToClass();
                    SaveFile(Path.Combine("Models", c.Name + ".cs"), c.ToString());
                    SaveFile(Path.Combine("Repositories", "I" + c.Name + "Repository.cs"), c.ToRepositoryInterfaceString());
                    SaveFile(Path.Combine(Path.Combine("Repositories", "Sql"), "Sql" + c.Name + "Repository.cs"), c.ToSqlRepositoryClassString());
                }
            } else {
                ShowHelp();
            }
        }
        
        void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine(@"Usage: .\sgen.exe g ''");
        }
        
        void SaveFile(string fileName, string content)
        {
            using (var w = new StreamWriter(fileName)) {
                w.WriteLine(content);
            }
        }
    }
}