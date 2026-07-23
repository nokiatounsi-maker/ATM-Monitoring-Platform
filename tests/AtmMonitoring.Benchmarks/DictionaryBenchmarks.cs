using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(100, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < ItemCount; i++)
        {
            var atm = new Atm
            {
                Id = $"ATM{i:D4}",
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000m,
                LastMaintenance = DateTime.UtcNow
            };
            _atms.TryAdd(atm.Id, atm);
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateWithValuesSnapshot()
    {
        int count = 0;
        // This creates a full snapshot of Values under the hood
        var values = _atms.Values;
        foreach (var val in values)
        {
            if (val != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int IterateDirectKeyValuePair()
    {
        int count = 0;
        // Direct iteration avoids the Values collection snapshot allocation
        foreach (var kvp in _atms)
        {
            if (kvp.Value != null)
            {
                count++;
            }
        }
        return count;
    }
}
