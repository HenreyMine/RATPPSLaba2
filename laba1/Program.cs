using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace laba1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var port = Console.ReadLine();
            var httpClient = new HttpClient();
            var url = $"http://127.0.0.1:{port}";
            while (!httpClient.Ping(url)) ;

            var serializer = new JsonSerializator();
            var input = serializer.Deserialize<Input>(httpClient.GetInputData(url));

            decimal sumResult = 0;
            var sortedInputs = new List<decimal>();
            foreach (var term in input.Sums)
            {
                sumResult += term;
                sortedInputs.Add(term);
            }
            sumResult *= input.K;
            var mulResult = 1;
            foreach (var multiplier in input.Muls)
            {
                mulResult *= multiplier;
                sortedInputs.Add(multiplier);
            }
            sortedInputs.Sort();
            var output = new Output
            {
                SumResult = sumResult,
                MulResult = mulResult,
                SortedInputs = sortedInputs.ToArray(),
            };

            httpClient.WriteAnswer(url, AddZeros(serializer.Serialize(output)));
        }

        public static string AddZeros(string serializedObject)
        {
            var indexOfFirstComma = serializedObject.IndexOf(',');
            var indexOfFirstPoint = serializedObject.IndexOf('.');
            if (indexOfFirstPoint > indexOfFirstComma)
            {
                serializedObject = serializedObject.Substring(0, indexOfFirstComma) + ".0" + serializedObject.Substring(indexOfFirstComma);
            }
            var indexOfFirstSquareBracket = serializedObject.IndexOf('[');
            var sortedInputs = serializedObject.Substring(indexOfFirstSquareBracket);
            sortedInputs = sortedInputs.Substring(0, sortedInputs.Length - 2);
            var sortedInputsList = sortedInputs.Split(',');
            for (var i = 0; i < sortedInputsList.Length; i++)
            {
                if (!sortedInputsList[i].Contains('.'))
                {
                    sortedInputsList[i] += ".0";
                }
            }
            sortedInputs = string.Join(",", sortedInputsList);
            return serializedObject.Substring(0, indexOfFirstSquareBracket) + sortedInputs + "]}";
        }
    }
}
