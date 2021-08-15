using System;
using System.Collections.Generic;
using System.Text;

namespace IEnumerable
{
    public class Car
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Displacement { get; set; }
        public int Cylinder { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }
    }

    public class CarStatistics
    {
        public CarStatistics()
        {
            MaxValue = int.MinValue;
            MinValue = int.MaxValue;
        }

        internal CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Count { get; private set; }
        public int Total {  get;  private set; }
        public double Average { get; set; }

        public CarStatistics Accumulate(Car c)
        {
            Count += 1;
            Total += c.Combined;
            MinValue = Math.Min(MinValue, c.Combined);
            MaxValue = Math.Max(MaxValue, c.Combined);
            return this;
        }
    }
}
