using BenchmarkDotNet.Running;

namespace AtmMonitoring.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<DictionaryBenchmarks>();
    }
}
