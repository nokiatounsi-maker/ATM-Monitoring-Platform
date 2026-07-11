using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm { Id = id, Location = "Location " + i });
        }
    }

    [Benchmark]
    public int ConsumeValues()
    {
        int count = 0;
        foreach (var item in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int ConsumeIteration()
    {
        int count = 0;
        foreach (var kvp in _atms)
        {
            count++;
            if (kvp.Value == null) count--; // just to use it
        }
        return count;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<AtmServiceBenchmarks>();
    }
}
