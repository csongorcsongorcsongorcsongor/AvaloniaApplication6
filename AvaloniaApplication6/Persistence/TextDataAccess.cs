using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication6.Models;

namespace AvaloniaApplication6.Persistence
{
    public class TextDataAccess : IDataAccess
    {
        public async Task Save(string path, Car car)
        {
            using StreamWriter writer = new StreamWriter(path, false);
            await writer.WriteLineAsync(car.ToString());
        }

        public async Task<Car?> Load(string path)
        {
            if (!File.Exists(path))
                return null;

            using StreamReader reader = new StreamReader(path);
            string? line = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line))
                return null;

            string[] data = line.Split(';');
            return new Car(
                data[0],
                data[1],
                int.Parse(data[2]),
                int.Parse(data[3])
            );
        }
    }
}
