using BenchmarkDotNet.Running;
using AtmMonitoring.Benchmarks;

Console.WriteLine("Starting AtmMonitoring performance benchmarks...");
BenchmarkRunner.Run<DictionaryBenchmarks>();
