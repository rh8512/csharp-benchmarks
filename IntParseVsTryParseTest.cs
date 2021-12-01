using BenchmarkDotNet.Attributes;
using System;

namespace csharp_benchmarks
{
    public class IntParseVsTryParseTest
    {
        [Params(1000000)]
        public int ItemsCount;

        private string[] stringToParse;

        [GlobalSetup]
        public void Init()
        {
            stringToParse = new string[ItemsCount];
            Random rand = new Random();
            for (int i = 0; i < ItemsCount; i++)
                stringToParse[i]= rand.Next(0, 100).ToString();
        }

        [Benchmark]
        public void ParseTest()
        {
            for (int i = 0; i < ItemsCount; i++)
                int.Parse(stringToParse[i]);
        }

        [Benchmark]
        public void TryParseTest()
        {
            for (int i = 0; i < ItemsCount; i++)
                int.TryParse(stringToParse[i], out _);
        }

        [Benchmark]
        public void ConvertTest()
        {
            for (int i = 0; i < ItemsCount; i++)
                Convert.ToInt32(stringToParse[i]);
        }
    }
}
