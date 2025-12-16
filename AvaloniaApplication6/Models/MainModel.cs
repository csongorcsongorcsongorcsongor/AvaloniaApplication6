using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Utilities;
using AvaloniaApplication6.Persistence;

namespace AvaloniaApplication6.Models
{
    public class MainModel
    {
        private IDataAccess _dataAccess;

        public event EventHandler<CarEventArgs> CarCreated;


        public List<Car> cars;
        public List<Car> removedCars;
        public MainModel(IDataAccess access) {
            cars = new List<Car>();
            removedCars = new List<Car>();
            _dataAccess = access;

        }

        public void Save(string path)
        {
            _dataAccess.Save(path, cars);

        }
        public async Task Load(string path)
        {
            List<Car> newCars = await _dataAccess.Load(path);
            foreach (Car p in newCars)
            {
                cars.Add(p);
                CarCreated.Invoke(this, new CarEventArgs(p));
            }
        }
    }
}
