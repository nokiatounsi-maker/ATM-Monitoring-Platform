using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<GetAllAtmsBenchmark>();
    }
}

[MemoryDiagnoser]
public class GetAllAtmsBenchmark
{
    private readonly ConcurrentDictionary<string, Atm> _dict = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D4}";
            _dict.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 10000m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int ValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _dict.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int DirectYieldEnumeration()
    {
        int count = 0;
        foreach (var atm in GetAtmsYield())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> GetAtmsYield()
    {
        foreach (var kvp in _dict)
        {
            yield return kvp.Value;
        }
    }
}
