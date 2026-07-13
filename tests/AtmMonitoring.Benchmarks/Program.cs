using BenchmarkDotNet.Running;

namespace AtmMonitoring.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<AtmServiceBenchmarks>();
    }
}
