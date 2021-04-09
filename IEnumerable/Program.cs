using System;
using System.Collections.Generic;
using System.Linq;
using IEnumerable;

namespace IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var str = "boys";
            var rev = "boys".Reverse();
            var count = str.Stringify();
            var num = new List<int>();
            var number = NumberList().Distinct();


            var query = from s in number
                        where s > 1
                        select new { Item = s };
            //for (int i = 0; i < number.Count(); i++)
            //{
            //    if (num.Contains(number[i]))
            //    {
            //        num.Remove(number[i]);
            //    }
            //    num.Add(number[i]);
            //}

            var suits = Suits();
            var ranks = Ranks();
            var nums = number.Where(x => x > 1);
            var startingDeck = from s in Suits()
                               from r in Ranks()
                               select new { Suit = s, Rank = r };

            // Display each card that we've generated and placed in startingDeck in the console
            foreach (var card in startingDeck)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine("Hello World!");

            var top = startingDeck.Take(26);
            var bottom = startingDeck.Skip(26);
            var shuffle = bottom.InterleaveSequenceWith(top);

            foreach (var c in shuffle)
            {
                Console.WriteLine(c);
            }

            var times = 0;
            // We can re-use the shuffle variable from earlier, or you can make a new one
            shuffle = startingDeck;
            do
            {
                shuffle = shuffle.Take(26).InterleaveSequenceWith(shuffle.Skip(26));

                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine();
                times++;

            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine(times);
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
