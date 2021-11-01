using BenchmarkDotNet.Running;
using csharp_benchmarks;


_ = BenchmarkRunner.Run<NestedForeachVsSelectMany>();
