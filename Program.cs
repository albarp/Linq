using System;
using System.Collections.Generic;
using System.Linq;
using static MoreLinq.Extensions.PairwiseExtension;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Range();

            // Sum();

            // CustomExtensionMethod();

            // MoreLinq();

            Console.ReadLine();

        }

        private static void Range()
        {
            // should output 1,2,3,4,5,6

            var ranges = "6,1-3,2-4"
                .Split(',')
                .Select(x => x.Split("-"))
                .Select(p => new { First = int.Parse(p[0]), Last = int.Parse(p.Last()) })
                .SelectMany(r => Enumerable.Range(r.First, r.Last - r.First + 1))
                .OrderBy(r => r)
                .Distinct();

            foreach (var item in ranges)
            {
                Console.WriteLine(item);
            }

        }

        private static void Sum()
        {
            var tracksDuration = "2:54,3:48,4:51,3:32,6:15,4:08,5:17,3:13,4:16,3:55,4:53,5:35,4:24";

            var albumDuration =
                tracksDuration
                .Split(",")
                .Select(d => TimeSpan.Parse("0: " + d))
                .Aggregate((acc, t) => acc + t);

            Console.WriteLine(albumDuration);
        }

        private static void MoreLinq()
        {
            var splitTimes = "00:45,01:32,02:15,03:01,03:44,04:31,05:19,06:01,06:47,07:35";

            var pairWiseDifferences = splitTimes
                .Split(",")
                .Select(x => TimeSpan.Parse("00:" + x))
                .Prepend(TimeSpan.Zero)
                .Pairwise((a, b) => b - a);

            foreach (var item in pairWiseDifferences)
            {
                Console.WriteLine(item);
            }
        }

        private static void CustomExtensionMethod()
        {
            var c = "Dog,Cat,Rabbit,Dog,Dog,Lizard,Cat,Cat,Dog,Rabbit,Guinea,Pig,Dog"
                            .Split(",")
                            .CountBy(x => (x == "Dog" || x == "Cat") ? x : "other");

            foreach (var x in c)
            {
                Console.WriteLine($"{x.Key} {x.Value}");
            }
        }
    }

    static class MyLinqExtensions
    {
        public static IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            var counts = new Dictionary<TKey, int>();
            foreach (var item in source)
            {
                var key = selector(item);
                if (!counts.ContainsKey(key))
                {
                    counts[key] = 1;
                }
                else
                {
                    counts[key]++;
                }
            }

            return counts;
        }
    }
}
