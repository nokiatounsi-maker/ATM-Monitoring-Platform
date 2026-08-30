using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private ConcurrentDictionary<string, Atm> _atms = new();

    [GlobalSetup]
    public void Setup()
    {
        _atms = new ConcurrentDictionary<string, Atm>();
        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int ValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int DirectYieldIteration()
    {
        int count = 0;
        foreach (var atm in IterateValues(_atms))
        {
            count++;
        }
        return count;
    }

    private static IEnumerable<Atm> IterateValues(ConcurrentDictionary<string, Atm> dict)
    {
        foreach (var kvp in dict)
        {
            yield return kvp.Value;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<AtmServiceBenchmarks>();
    }
}
