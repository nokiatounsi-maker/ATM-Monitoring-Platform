using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(10, 100, 1000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < Count; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateWithValues()
    {
        int sum = 0;
        // Accessing .Values creates a snapshot array/list under the hood
        foreach (var atm in _atms.Values)
        {
            if (atm.Status == AtmStatus.Online)
            {
                sum++;
            }
        }
        return sum;
    }

    [Benchmark]
    public int IterateDirectlyWithYield()
    {
        int sum = 0;
        foreach (var atm in GetAtmsDirect())
        {
            if (atm.Status == AtmStatus.Online)
            {
                sum++;
            }
        }
        return sum;
    }

    private IEnumerable<Atm> GetAtmsDirect()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
