using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
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
            var id = $"ATM{i:D3}";
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
    public int UseValuesSnapshot()
    {
        int count = 0;
        // Accessing .Values forces a snapshot copy of the collection to be allocated on the heap.
        var values = _atms.Values;
        foreach (var item in values)
        {
            if (item != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int UseDirectIterator()
    {
        int count = 0;
        // Iterating using yield return avoids heap allocations by iterating elements directly without snapshot.
        foreach (var item in GetAllAtmsDirect())
        {
            if (item != null)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerable<Atm> GetAllAtmsDirect()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
