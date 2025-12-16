using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication6.Models;

namespace AvaloniaApplication6.Persistence
{
    public interface IDataAccess
    {
        Task Save(string path, Car car);
        Task<Car?> Load(string path);
    }
}
