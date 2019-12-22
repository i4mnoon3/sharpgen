using System;
using SharpGenerator.Models;

namespace SharpGenerator.Repositories
{
    public interface IDatabaseRepository
    {
        Database Read(string name);
    }
}
