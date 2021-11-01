using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;

namespace csharp_benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 10)]
    public class ForLoopTest
    {
        [Params(1000)]
        public int ItemsCount;

        private Appointment[] appointmentsArray;

        [GlobalSetup]
        public void Init()
        {
            appointmentsArray = new Appointment[ItemsCount];

            for (int i = 0; i < ItemsCount; i++)
                appointmentsArray[i] = new Appointment() { ServiceName = Guid.NewGuid().ToString() }; ;
        }

        [Benchmark]
        public void For()
        {
            for (int i = 0; i < appointmentsArray.Length; i++)
            {
               Console.WriteLine(appointmentsArray[i].ServiceName);
            }
        }

        [Benchmark]
        public void ForLengthOut()
        {
            int length = appointmentsArray.Length;
            for (int i = 0; i < length ; i++)
            {
                Console.WriteLine(appointmentsArray[i].ServiceName);
            }
        }
    }
}
