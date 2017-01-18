namespace Solver
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    class Program
    {
        class TestCase
        {
            public string Name;
            public int Answer;

            private EdgeGen testData;
            public Action<EdgeGen> Setup;

            private void Run(int times, BaseSolver solver)
            {
                var list = new List<TimeSpan>();

                for (var i = 0; i < times; i++)
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var answer = solver.Solve(testData, testData.GetMaxNode());
                    sw.Stop();
                    list.Add(sw.Elapsed);

                    if (answer != Answer)
                    {
                        throw new Exception("Invalid Answer");
                    }
                }

                var max = list.Max();
                var min = list.Min();
                var mean = TimeSpan.FromMilliseconds(list.Select(d => d.TotalMilliseconds).Sum() / list.Count);

                Console.WriteLine("{0}({1})\t\t Count: {2}\tDuration: Min {3}\t Max:{4}\t Ave: {5}",
                   Name,
                   solver.GetType().Name,
                   times,
                   min.TotalMilliseconds,
                   max.TotalMilliseconds,
                   mean.TotalMilliseconds);
            }
            public void Run(int times)
            {
                Run(times, new FastFindSolver());
                Run(times, new FastUnionSolver());
            }

            public void Init()
            {
                var edgeGen = new EdgeGen();
                Setup(edgeGen);
                testData = edgeGen;
            }
        }

        static void Main()
        {
            var tests = new List<TestCase>()
            {
                new TestCase
                {
                    Name = "Grass Forest",
                    Answer = 60 * 1000 * 1000,
                    Setup = gen =>gen.Create(60 * 1000 * 1000, o => new Edge(gen.GetNode(), gen.GetNode())),
                },

                new TestCase
                {
                    Name = "Single Tree",
                    Answer = 1 ,
                    Setup = gen =>
                    {
                        var root = gen.GetNode();
                        gen.Create(60*1000*1000, o => new Edge(root, gen.GetNode()));
                    },
                },

                new TestCase
                {
                    Name = "Random Tree",
                    Answer = 6*1000*1000 ,
                    Setup = gen =>
                    {
                        var random = new Random();

                        for (int i = 0; i < 6*1000*1000; i++)
                        {
                            var root = gen.GetNode();
                            gen.AddNodesFrom(root, random.Next(10));
                        }
                    },
                },
            };

            Console.WriteLine("Setting up test data.");
            foreach (var testCase in tests)
            {
                testCase.Init();
            }

            Console.WriteLine("Running all tests once to remove the impact of JIT.");
            foreach (var testCase in tests)
            {
                testCase.Run(1);
            }

            foreach (var testCase in tests)
            {
                Console.WriteLine(testCase.Name);
                var times = 4;
                testCase.Run(times);
           }
        }
    }
}
