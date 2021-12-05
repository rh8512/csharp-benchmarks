using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp_benchmarks
{
    public class LinqWhereAndTest
    {
        [Params(10)]
        public int ItemsCount;

        private List<Appointment> appointments;
        private DateTime now;

        [GlobalSetup]
        public void Init()
        {
            now = DateTime.Now;
            appointments = new List<Appointment>();
            for (int i = 0; i < ItemsCount; i++)
            {
                appointments.Add(new() { IsCanceled = false, CancalationDate=now});
            }
        }

        [Benchmark]
        public void MultipleWhere()
        {
            _ = from appointment in appointments where appointment.IsCanceled == false && appointment.CancalationDate < DateTime.Now select appointment;
        }

        [Benchmark]
        public void WhereAndOperator()
        {
            _ = from appointment in appointments where appointment.IsCanceled == false where appointment.CancalationDate < DateTime.Now select appointment;     
        }

        [Benchmark]
        public void MultipleWhereEmptyList()
        {
            _ = from appointment in appointments where appointment.IsCanceled == true && appointment.CancalationDate < DateTime.Now select appointment;
        }

        [Benchmark]
        public void WhereAndOperatorEmptyList()
        {
            _ = from appointment in appointments where appointment.IsCanceled == true where appointment.CancalationDate < DateTime.Now select appointment;
        }


        class Appointment
        {
            public bool IsCanceled { get; set; }
            public DateTime CancalationDate { get; set; }
        }
    }
}
