using System;
using System.Collections.Generic;
using SharpGenerator.Models;

namespace SharpGenerator.Repositories
{
    public interface ITableRepository
    {
        List<Table> FindByDatabase(string databaseName);
    }
}
