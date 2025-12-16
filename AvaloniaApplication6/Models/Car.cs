using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AvaloniaApplication6.Models
{
    public class Car
    {
        public string Model { get; private set; }
        public string Type { get; private set; }
        public int Age { get; private set; }
        public int Miles { get; private set; }

        public Car(string model, string type, int age, int miles)
        {
            Model = model;
            Type = type;
            Age = age;
            Miles = miles;
        }
        public override string ToString()
        {
            return Model + ";" + Type + ";" + Age + ";" + Miles;
        }
    }
}
