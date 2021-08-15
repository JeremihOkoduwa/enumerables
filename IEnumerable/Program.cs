using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using IEnumerable;


namespace IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarsDb>()); 
            
            var cars = ProcessCsvFile("fuel.csv");
            InsertCars(cars);
            var manufacturer = ProcessManufactures("manufacturers.csv");
            var groupCars = cars.GroupBy(x => x.Manufacturer).Select(x =>
            {
                var result = x.Aggregate(new CarStatistics(), (acc, c) => acc.Accumulate(c), acc => acc.Compute());
               
                return new
                {

                    Name = x.Key,
                    Average = result.Average,
                    Min = result.MinValue,
                    Max = result.MaxValue
                };
            }).OrderByDescending(x => x.Max);

            foreach (var item in groupCars)
            {
                Console.WriteLine($"{item.Name}, {item.Max}");
            }
            var joined = cars.Join(manufacturer, x => new { x.Manufacturer, x.Year}, m =>new { Manufacturer = m.Name,  m.Year}, (x, m) => new
            {
                m.Headquaters,
                x.Name,
                x.Combined
            }).ToList();

            foreach (var item in joined)
            {
                Console.WriteLine($"{item.Name} - {item.Headquaters} - {item.Combined}");
            }
           

            //PrinListOfCars(cars);
            var path = @"C:\windows";
            ShowFilesWithLink(path);
            ShowLargeFiles(path);
            //var buffer = Buffer.ByteLength(NumberList().ToArray());
            
            //var str = "boys";
            //var rev = "boys".Reverse();
            //var count = str.Stringify();
            //var num = new List<int>();
            //var number = NumberList().Distinct();


            //var query = from s in number
            //            where s > 1
            //            select new { Item = s };
            //for (int i = 0; i < number.Count(); i++)
            //{
            //    if (num.Contains(number[i]))
            //    {
            //        num.Remove(number[i]);
            //    }
            //    num.Add(number[i]);
            //}

            //var suits = Suits();
            //var ranks = Ranks();
            //var nums = number.Where(x => x > 1);
            //var startingDeck = from s in Suits()
            //                   from r in Ranks()
            //                   select new { Suit = s, Rank = r };

            // Display each card that we've generated and placed in startingDeck in the console
            //foreach (var card in startingDeck)
            //{
            //    Console.WriteLine(card);
            //}
            //Console.WriteLine("Hello World!");

            //var top = startingDeck.Take(26);
            //var bottom = startingDeck.Skip(26);
            //var shuffle = bottom.InterleaveSequenceWith(top);

            //foreach (var c in shuffle)
            //{
            //    Console.WriteLine(c);
            //}

            //var times = 0;
            //// We can re-use the shuffle variable from earlier, or you can make a new one
            //shuffle = startingDeck;
            //do
            //{
            //    shuffle = shuffle.Take(26).InterleaveSequenceWith(shuffle.Skip(26));

            //    foreach (var card in shuffle)
            //    {
            //        Console.WriteLine(card);
            //    }
            //    Console.WriteLine();
            //    times++;

            //} while (!startingDeck.SequenceEquals(shuffle));

            //Console.WriteLine(times);
        }

        private static void InsertCars(List<Car> cars)
        {
            var db = new CarsDb();
            if (!db.Cars.Any())
            {
                foreach (var item in cars)
                {
                    db.Cars.Add(item);
                    Console.WriteLine("Item added");
                }
                db.SaveChanges();
            }
        }

        private static List<Manufacturers> ProcessManufactures(string path)
        {
            var readAllLine = File.ReadAllLines(path);
            var read = readAllLine.Where(x => x.Length > 1).Select(TransformToManufacturer);
            //var readAllLines = File.ReadAllLines(path).Skip(1).Where(x => x.Length > 1)
            //     .Select(x => {

            //         var columns = x.Split(",");
            //         return new Manufacturers
            //         {
            //             Name = columns[0],
            //             Headquaters = columns[1],
            //             Year = Convert.ToInt32(columns[1]),
            //         };
            //     });
            return read.ToList();
        }

        private static Manufacturers TransformToManufacturer(string line)
        {
            var columns = line.Split(",");
            return new Manufacturers
            {
                Name = columns[0],
                Headquaters = columns[1],
                Year = Convert.ToInt32(columns[2]),
            };
        }

        private static void PrinListOfCars(List<Car> cars)
        {

            foreach (var item in cars.OrderByDescending(x => x.Combined).ThenBy(x => x.Name).Take(10))
            {
                Console.WriteLine(item.Name);
            }
        }

        private static List<Car> ProcessCsvFile(string path)
        {
            var readAllLines = File.ReadAllLines(path).Skip(1).Where(x => x.Length > 1)
                .Select(TransformToCar);

            return readAllLines.ToList();
        }

        private static Car TransformToCar(string line)
        {
            var columns = line.Split(",");
            return new Car
            {
                Year = Convert.ToInt32(columns[0]),
                Manufacturer = columns[1],
                Name = columns[2],
                Displacement = double.Parse(columns[3]),
                Cylinder = int.Parse(columns[4]),
                City = int.Parse(columns[5]),
                Highway = int.Parse(columns[6]),
                Combined = int.Parse(columns[7])


            };
        }

        private static void ShowFilesWithLink(string path)
        {
            var movies = new List<Movie>
            {
               new Movie
                {
                   Name = "Jason Bourne",
                   Year = 2016,
                   Ratings = 50.00m
                },
               new Movie
                {
                   Name = "The Bourne Identity",
                   Year = 2002,
                   Ratings = 90m
                },
               new Movie
                {
                   Name = "The Bourne Supremacy",
                   Year = 2004,
                   Ratings = 90m
                },
               new Movie
                {
                   Name = "The Bourne Ultimatum",
                   Year = 2007,
                   Ratings = 80m
                }
            };

            Func<Movie, bool> function = x => x.Year > 2005;
            var moviequery = movies.Filter(function);

            foreach (var item in moviequery)
            {
                Console.WriteLine($"name - {item.Name}");
            }
            var query = new DirectoryInfo(path).GetFiles().OrderByDescending(x => x.Length).Take(5);
            var list = new DirectoryInfo(path).GetFiles();
            var fileInfos = list.AsEnumerable().GetEnumerator();


            while (fileInfos.MoveNext())
            {
                Console.WriteLine($"{fileInfos.Current.Name}-{fileInfos.Current.Length}-count{list.Counts()}");
            }
        }
        private static void ShowLargeFiles(string path)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                var files = directory.GetFiles();

                foreach (var item in files)
                {
                    Console.WriteLine($"{item.Name}-{item.Length}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        static IEnumerable<T> InterLeave<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
            }
        }
        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }

        static IEnumerable<int> NumberList()
        {
            yield return 1;
            yield return 4;
            yield return 5;
            yield return 5;
            yield return 5;
            yield return 3;
            yield return 10;
            yield return 11;
            yield return 12;
            yield return 13;
            yield return 14;
            yield return 15;
            yield return 16;
        }
    }

    
}
