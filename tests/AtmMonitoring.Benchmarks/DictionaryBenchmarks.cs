using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
[HideColumns("Job", "RatioSD", "Error", "StdDev")]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < ItemCount; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 5000.00m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int AccessValuesSnapshot()
    {
        int count = 0;
        // This accesses .Values, causing a snapshot allocation
        foreach (var atm in _atms.Values)
        {
            if (atm != null) count++;
        }
        return count;
    }

    [Benchmark]
    public int DirectYieldReturn()
    {
        int count = 0;
        // This uses the yield return implementation that avoids .Values snapshot
        foreach (var atm in GetAtmsDirect())
        {
            if (atm != null) count++;
        }
        return count;
    }

    private IEnumerable<Atm> GetAtmsDirect()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
