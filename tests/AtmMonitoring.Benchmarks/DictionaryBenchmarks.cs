using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _dictionary = new();
    private readonly List<Atm> _list = new();

    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _dictionary.Clear();
        _list.Clear();
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
            _dictionary.TryAdd(atm.Id, atm);
            _list.Add(atm);
        }
    }

    [Benchmark(Baseline = true)]
    public List<Atm> GetValues_Snapshot()
    {
        // Accessing .Values forces a snapshot of the entire ConcurrentDictionary,
        // which allocates a new collection and copies elements.
        return new List<Atm>(_dictionary.Values);
    }

    [Benchmark]
    public List<Atm> GetValues_DirectIteration()
    {
        // Direct iteration (using foreach on ConcurrentDictionary KeyValuePairs)
        // avoids copying the values to a new intermediate collection.
        var list = new List<Atm>(ItemCount);
        foreach (var kvp in _dictionary)
        {
            list.Add(kvp.Value);
        }
        return list;
    }
}
