using System;
using System.Collections.Generic;
using SharpGenerator.Models;

namespace SharpGenerator.Repositories
{
    public interface IColumnRepository
    {
        List<Column> FindByTable(string tableName, string databaseName);
        Column ReadPrimaryKey(string tableName);
    }
}
