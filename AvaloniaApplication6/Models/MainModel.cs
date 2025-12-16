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
        private readonly IDataAccess _dataAccess;

        public event EventHandler<CarEventArgs>? CarLoaded;

        public Car? CurrentCar { get; private set; }

        public MainModel(IDataAccess access)
        {
            _dataAccess = access;
        }

        public void SetCar(Car car)
        {
            CurrentCar = car;
        }

        public async Task Save(string path)
        {
            if (CurrentCar != null)
                await _dataAccess.Save(path, CurrentCar);
        }

        public async Task Load(string path)
        {
            CurrentCar = await _dataAccess.Load(path);
            if (CurrentCar != null)
                CarLoaded?.Invoke(this, new CarEventArgs(CurrentCar));
        }
    }
}
