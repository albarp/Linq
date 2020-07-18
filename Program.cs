using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = "Dog,Cat,Rabbit,Dog,Dog,Lizard,Cat,Cat,Dog,Rabbit,Guinea,Pig,Dog"
                .Split(",")
                .CountBy(x => (x == "Dog" || x == "Cat") ? x : "other");

            foreach (var x in c)
            {
                Console.WriteLine($"{x.Key} {x.Value}");
            }

            Console.ReadLine();

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
