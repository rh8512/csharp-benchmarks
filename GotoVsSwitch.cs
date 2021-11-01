using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;

namespace csharp_benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 5)]
    public class GotoVsSwitch
    {

        private int _a;
        private int[,,] _magicNumbers;
        private int _compare = 9;
        [GlobalSetup]
        public void Init()
        {
            Random rnd = new Random();
            _a = rnd.Next(1, 3);

            _magicNumbers = new int[2, 2, 2] {{ { 1, 2 }, { 3, 4 } }, { { 7, 7 }, { 9, 7 } } };
        }

        [Benchmark]
        public void Switch()
        {
            switch (_a) { 
                case 1:
                    Console.WriteLine("1");
                    break;
                case 2:
                    Console.WriteLine("2");
                    break;
                default:
                    Console.WriteLine("n/n");
                    break;
            }
        }

        [Benchmark]
        public void Goto()
        {
                if (_a == 1)
                {
                    goto One;
                }
                else if (_a == 2)
                {
                    goto Two;
                }
                else
                {
                    goto Nn;
                }
                One: Console.WriteLine("One");
                Two: Console.WriteLine("Two");
                Nn: Console.WriteLine("n/n");
            
        }

        [Benchmark]
        public void BreakUsingGoto()
        {
            for (int i = 0; i < _magicNumbers.GetLength(0); ++i)
            {
                for (int j = 0; j < _magicNumbers.GetLength(1); ++j)
                {
                    for (int k = 0; k < _magicNumbers.GetLength(2); ++k)
                    {
                        if (_magicNumbers[i, j, k] == _compare) goto Exit;
                    }
                }
            }
            Exit: Console.WriteLine("exit"); 
        }

        [Benchmark]
        public void BreakUsingBreak()
        {
            bool exit = false;

            for (int i = 0; i < _magicNumbers.GetLength(0); ++i)
            {
                for (int j = 0; j < _magicNumbers.GetLength(1); ++j)
                {
                    for (int k = 0; k < _magicNumbers.GetLength(2); ++k)
                    {
                       if (_magicNumbers[i, j, k] == _compare) exit = true;
                    }
                    if (exit)
                        break;
                }
                if (exit)
                    break;
            }
            Console.WriteLine("exit");
        }
    }
}