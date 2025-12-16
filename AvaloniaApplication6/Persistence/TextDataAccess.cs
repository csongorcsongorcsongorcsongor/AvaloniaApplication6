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
        public async Task<List<Car>> Load(string path)
        {
            List<Car> cars = new List<Car>();
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    string numberOfPeople = await reader.ReadLineAsync();

                    for (int i = 0; i < int.Parse(numberOfPeople); i++)
                    {
                        string line = await reader.ReadLineAsync();
                        string[] data = line.Split(";");
                        Car car = new Car(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]));
                        cars.Add(car);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return cars;
        }

        public async Task Save(string path, List<Car> people)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                try
                {
                    string num = people.Count.ToString();
                    await writer.WriteLineAsync(num);
                    for (int i = 0; i < people.Count; i++)
                    {
                        string line = people[i].ToString();
                        await writer.WriteLineAsync(line);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
