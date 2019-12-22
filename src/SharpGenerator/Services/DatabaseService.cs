using System;
using SharpGenerator.Models;
using SharpGenerator.Repositories;

namespace SharpGenerator.Services
{
    public class DatabaseService
    {
        IDatabaseRepository databaseRepository;
        ITableRepository tableRepository;
        IColumnRepository columnRepository;
        
        public DatabaseService(IDatabaseRepository databaseRepository, ITableRepository tableRepository, IColumnRepository columnRepository)
        {
            this.databaseRepository = databaseRepository;
            this.tableRepository = tableRepository;
            this.columnRepository = columnRepository;
        }
        
        public Database GetDatabase(string name)
        {
            var d = databaseRepository.Read(name);
            if (d != null) {
                d.AddTables(tableRepository.FindByDatabase(name));
                foreach (var t in d.Tables) {
                    t.Columns = columnRepository.FindByTable(t.Name, name);
                    t.AddKey(columnRepository.ReadPrimaryKey(t.Name));
                }
            }
            return d;
        }
    }
}
