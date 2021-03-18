using BenchmarkDotNet.Running;

namespace csharp_benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {

            var results = BenchmarkRunner.Run<StringReplaceTest>();

            //BenchmarkSwitcher
            //.FromAssembly(typeof(Program).Assembly)
            //.Run(args,
            //    DefaultConfig.Instance
            //        .With(new EtwProfiler()));
            //var results = BenchmarkRunner.Run<StringReplaceTest>();
        }
    }
}
