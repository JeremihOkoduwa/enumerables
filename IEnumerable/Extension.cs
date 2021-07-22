using System;
using System.Collections.Generic;
using System.Text;

namespace IEnumerable
{
    public static class Extension
    {
        public static IEnumerable<T> InterleaveSequenceWith<T>
    (this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                yield return firstIter.Current;
                yield return secondIter.Current;
            }
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var list = new List<TSource>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }

            return list;

        }
        public static int Counts<T>(this IEnumerable<T> obj)
        {
            try
            {
                var count = 0;
                foreach (var item in obj)
                {
                    count += 1;
                }
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Stringify(this string obj)
        {
            var res = obj.ToCharArray().Length-1;
            return res;
        }

        public static string Reverse(this string obj)
        {
            var res = obj.ToCharArray();
            char[] result = new char[res.Length];
            for (int i = 0, j = res.Length -1; i < res.Length; j--, i++)
            {
                result[i] = res[j];
            }
            return new string(result);
        }

        public static bool SequenceEquals<T>
  (this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIter = first.GetEnumerator();
            var secondIter = second.GetEnumerator();

            while (firstIter.MoveNext() && secondIter.MoveNext())
            {
                if (!firstIter.Current.Equals(secondIter.Current))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
