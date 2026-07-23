using BenchmarkDotNet.Running;

namespace AtmMonitoring.Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<DictionaryBenchmarks>();
    }
}
