using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _dictionary = new();

    [Params(100, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _dictionary.Clear();
        for (int i = 0; i < ItemCount; i++)
        {
            var id = $"ATM{i:D4}";
            _dictionary.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000m + i,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int GetAllAtms_WithValuesSnapshot()
    {
        // Mimic accessing .Values which allocates an ICollection snapshot
        int count = 0;
        foreach (var atm in _dictionary.Values)
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int GetAllAtms_WithYieldReturn()
    {
        // Mimic our optimized yield return iteration directly over the ConcurrentDictionary
        int count = 0;
        foreach (var atm in IterateDirectly())
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerable<Atm> IterateDirectly()
    {
        foreach (var kvp in _dictionary)
        {
            yield return kvp.Value;
        }
    }
}
