using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;

namespace csharp_benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    public class ForeachVsForTest
    {
        [Params(1000, 10000)]
        public int ItemsCount;

        private Appointment[] appointmentsArray;
        private List<Appointment> appointmentsList;

        [GlobalSetup]
        public void Init()
        {
            appointmentsArray = new Appointment[ItemsCount];
            appointmentsList = new();

            for (int i = 0; i < ItemsCount; i++)
            {
                var appointment = new Appointment() { ServiceName = Guid.NewGuid().ToString() };
                appointmentsArray[i] = appointment;
                appointmentsList.Add(appointment);
            }
        }

        [Benchmark]
        public void ForOnArray()
        {
            for (int i = 0; i < appointmentsArray.Length; i++)
            {
                Console.WriteLine(appointmentsArray[i].ServiceName);
            }
        }

        [Benchmark]
        public void ForeachOnArray()
        {
            foreach (var appointment in appointmentsArray)
            {
                Console.WriteLine(appointment.ServiceName);
            }
        }

        [Benchmark]
        public void ForOnList()
        {
            for (int i = 0; i < appointmentsList.Count; i++)
            {
                Console.WriteLine(appointmentsList[i].ServiceName);
            }
        }

        [Benchmark]
        public void ForeachOnList()
        {
            foreach (var appointment in appointmentsList)
            {
                Console.WriteLine(appointment.ServiceName);
            }
        }

    }

    class Appointment
    {
        public string ServiceName { get; set; }
    }
}
