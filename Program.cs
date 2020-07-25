using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using static MoreLinq.Extensions.PairwiseExtension;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            SunstoneFIHR();

            //Range();

            // Sum();

            // CustomExtensionMethod();

            // MoreLinq();

            Console.ReadLine();

        }

        private static async void SunstoneFIHR()
        {
            JObject respDynamic = JObject.Parse(File.ReadAllText(@"org.json"));

            var y = respDynamic
                .SelectToken("$..telecom");

            dynamic administratorTelecom = respDynamic
                .SelectToken("$..telecom")
                .Where(telecom => telecom.Value<string>("system") == "Email")
                .OrderBy(telecom => telecom.Value<int>("rank"))
                .FirstOrDefault();

            var administratorMail = administratorTelecom?.value;

            // Questo funziona
            //var mail = respDynamic
            //    .SelectToken("$..telecom")
            //    .Select(contact => new { System = contact.Value<string>("system"), Value = contact.Value<string>("value") })
            //    .Where(c => c.System == "Email")
            //    .FirstOrDefault();


            //foreach (dynamic item in contacts)
            //{
            //    string system = item.system;
            //}

            //using (HttpClient httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Add("eHRSys", "o4a");
            //    httpClient.DefaultRequestHeaders.Add("CorrelationId", Guid.NewGuid().ToString());
            //    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "8155bde6b68b475eb085cacaf1053258");

            //    httpClient.BaseAddress = new Uri("https://snsapi-d.fmcgds-np.com");

            //    var request = new HttpRequestMessage(HttpMethod.Get, "/api/fhir/organizations/46dfa88d-78f5-4862-9c9b-2fff477bd9e6");

            //    var res = await httpClient.SendAsync(request);

            //    var respBody = await res.Content.ReadAsStringAsync();

            //    JObject resp = JsonConvert.DeserializeObject<JObject>(respBody);

            //    //resp.se

            //    dynamic respDynamic = JsonConvert.DeserializeObject(respBody);

            //    dynamic contacts = respDynamic.SelectToken("$..telecom");

            //    foreach (var item in contacts)
            //    {
            //        string system = item.system;
            //    }

            //    //IEnumerable<JToken> orgProperties =  resp.Children();

            //    //var contacts = ((JArray)respDynamic.SelectToken("$..telecom"))
            //    //    .Select(j => new { System = j.Value<string>("system"), Value = j.Value<string>("value") });

            //    //var y = contacts.Select(j => new { System = j.Value<string>("system"), Value = j.Value<string>("value") });

            //    //var contact = contacts.ElementAt(0);
            //    //.Select(j => new { System = j.Value<string>("system"), Value = j.Value<string>("value")});

            //    //JToken jToken = JObject.Parse(respBody);

            //    //jToken.SelectTokens()

            //    // var c = respDynamic.contact[0].telecom;

            //    //var x = c.Select(c => c.telecom);
            //}
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
