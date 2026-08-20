using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class ConcurrentDictionaryBenchmark
{
    private readonly ConcurrentDictionary<string, Atm> _dict = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            var atm = new Atm
            {
                Id = $"ATM{i:D4}",
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000m,
                LastMaintenance = DateTime.UtcNow
            };
            _dict.TryAdd(atm.Id, atm);
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
    public int YieldReturnIteration()
    {
        int count = 0;
        foreach (var atm in GetYieldReturn())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> GetYieldReturn()
    {
        foreach (var kvp in _dict)
        {
            yield return kvp.Value;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<ConcurrentDictionaryBenchmark>();
    }
}
