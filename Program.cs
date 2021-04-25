using BenchmarkDotNet.Running;
using csharp_benchmarks;



//BenchmarkRunner.Run<StringReplaceTest>();

var result = BenchmarkRunner.Run<ForeachVsForTest>();

//BenchmarkSwitcher
//.FromAssembly(typeof(Program).Assembly)
//.Run(args,
//    DefaultConfig.Instance
//        .With(new EtwProfiler()));
//var results = BenchmarkRunner.Run<StringReplaceTest>();
