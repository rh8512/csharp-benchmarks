using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Linq;

namespace csharp_benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    public class NestedForeachVsSelectMany
    {
        [Params(10000)]
        public int ItemsCount;

        private Appointment[] appointmentsArray;
        private AppointmentEvent[] appointmentEventsArray;

        [GlobalSetup]
        public void Init()
        {
            appointmentsArray = new Appointment[ItemsCount];
            appointmentEventsArray = new AppointmentEvent[ItemsCount];

            for (int i = 0; i < ItemsCount; i++)
            {
                var appointment = new Appointment() { ServiceName = Guid.NewGuid().ToString() };
                var appointmentEvent = new AppointmentEvent() { Name = Guid.NewGuid().ToString() };
                appointmentsArray[i] = appointment;
                appointmentEventsArray[i] = appointmentEvent;
            }
        }

        [Benchmark]
        public void NestedForachOnArray()
        {
            foreach(var appointment in appointmentsArray)
            {
                foreach (var appointmentEvent in appointmentEventsArray)
                {
                    Console.WriteLine(appointment.ServiceName + appointmentEvent.Name);
                }
            }
        }

        [Benchmark]
        public void SelectMany()
        {
            var flat = appointmentsArray.SelectMany(b => appointmentEventsArray, (a, b) => (a.ServiceName + b.Name));

            foreach (var item in flat)
            {
                Console.WriteLine(item);
            }
        }


        [Benchmark]
        public void Zip()
        {
            foreach (var flatItem in appointmentsArray.Zip(appointmentEventsArray, (a,b)=> a.ServiceName + b.Name))
            {
                Console.WriteLine(flatItem);
            }
        }

    }

    class AppointmentEvent
    {
        public string Name { get; set; }
    }
}
