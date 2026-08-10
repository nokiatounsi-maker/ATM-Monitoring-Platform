using BenchmarkDotNet.Running;
using AtmMonitoring.Benchmarks;

namespace AtmMonitoring.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<DictionaryBenchmarks>();
    }
}
