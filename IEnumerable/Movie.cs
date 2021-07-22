using System;
using System.Collections.Generic;
using System.Text;

namespace IEnumerable
{
    public class Movie
    {
        int _year;
        public string Name { get; set; }
        public decimal Ratings { get; set; }
        public int Year {
            get {
                Console.WriteLine($"Returning year - {_year} and Title({this.Name})");
                return _year;
            }  set { _year = value; } }


    }
}
