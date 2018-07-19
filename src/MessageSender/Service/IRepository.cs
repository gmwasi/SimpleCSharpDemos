using System.Collections.Generic;
using Main.Models;

namespace Main.Service
{
    public interface IRepository
    {
        IEnumerable<Table> Get();
    }
}