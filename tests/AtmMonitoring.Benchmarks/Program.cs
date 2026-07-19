using BenchmarkDotNet.Running;
using AtmMonitoring.Benchmarks;

var summary = BenchmarkRunner.Run<DictionaryBenchmarks>();
