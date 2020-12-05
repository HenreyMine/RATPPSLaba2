using System;
using laba1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void JsonSerialize()
        {
            var input = new laba1.Input
            {
                K = 35,
                Sums = new[] { 1m, 2m, 3.5m, 4.8m, 9.999m, 99m },
                Muls = new[] { 1, 2, 34, 42, 34, 41243 },
            };
            var serialize = new laba1.JsonSerializator();
            var serializedObject = serialize.Serialize(input);
            Assert.AreEqual(serializedObject,
                "{\"K\":35,\"Sums\":[1,2,3.5,4.8,9.999,99],\"Muls\":[1,2,34,42,34,41243]}");
        }
        [TestMethod]
        public void JsonDeSerialize()
        {
            var serialize = new laba1.JsonSerializator();
            var deserializedObject = serialize.Deserialize<laba1.Input>(
                "{\"K\":35,\"Sums\":[1,2,3.5,4.8,9.999,99],\"Muls\":[1,2,34,42,34,41243]}");
            var flag = true;
            var input = new laba1.Input
            {
                K = 35,
                Sums = new[] { 1m, 2m, 3.5m, 4.8m, 9.999m, 99m },
                Muls = new[] { 1, 2, 34, 42, 34, 41243 },
            };
            if (input.K != deserializedObject.K)
            {
                flag = false;
            }
            for (var i = 0; i < input.Sums.Length; i++)
            {
                if (input.Sums[i] != deserializedObject.Sums[i])
                {
                    flag = false;
                }
            }
            for (var i = 0; i < input.Muls.Length; i++)
            {
                if (input.Muls[i] != deserializedObject.Muls[i])
                {
                    flag = false;
                }
            }
            Assert.AreEqual(flag, true);
        }
        [TestMethod]
        public void AddZerosJson()
        {
            var json = "{\"SumResult\":30,\"MulResult\":4,\"SortedInputs\":[1,1.01,2.02,4]}";
            json = laba1.Program.AddZeros(json);
            Assert.AreEqual("{\"SumResult\":30.0,\"MulResult\":4,\"SortedInputs\":[1.0,1.01,2.02,4.0]}", json);
        }
        [TestMethod]
        public void Ping()
        {
            var httpClient = new HttpClient();
            var answer = httpClient.Ping("https://tolltech.ru/study");
            Assert.AreEqual(answer, true);
        }
    }
}
