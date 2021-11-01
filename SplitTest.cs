using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;

namespace csharp_benchmarks
{
    public class SplitTest
    {
        [Params(100000)]
        public int ItemsCount;

        private string longStringToSplit;

        [GlobalSetup]
        public void Init()
        {    
            for (int i = 0; i < ItemsCount; i++)
                longStringToSplit += $"{Guid.NewGuid()};";
        }

        [Benchmark]
        public void Split()
        {
            _ = longStringToSplit.Split(";");
        }

        [Benchmark]
        public void SplitChar()
        {
            _ = longStringToSplit.Split(new char[] { ';' });
        }

        [Benchmark]
        public void SplitCharNew()
        {
            _ = longStringToSplit.Split(';', StringSplitOptions.None);
        }
    }
}
