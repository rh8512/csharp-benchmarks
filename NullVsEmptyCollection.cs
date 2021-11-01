using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 10)]
    public class NullVsEmptyCollection
    {

        private A _a;

        [GlobalSetup]
        public void Init()
        {
            _a = new();
        }

        [Benchmark]
        public void ReturnNull()
        {
            _a.ReturnNull();
        }

        [Benchmark]
        public void ReturnEmptyList()
        {
            _a.ReturnEmptyList();
        }

        [Benchmark]
        public void ReturnEmptyEnumerable()
        {
            _a.ReturnEmptyEnumerable();
        }

        [Benchmark]
        public void ReturnYieldBreak()
        {
            _a.ReturnYieldBreak();
        }
    }



    class A
    {
        public List<B> ReturnNull() => null;

        public IList<B> ReturnEmptyList() => new List<B>();

        public IEnumerable<B> ReturnEmptyEnumerable() => Enumerable.Empty<B>();


        public IEnumerable<B> ReturnYieldBreak()
        {
            yield break;
        }
    }

    class B
    {
    }
}
