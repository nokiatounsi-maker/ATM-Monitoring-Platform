using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(10, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < ItemCount; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int IteratingValuesProperty()
    {
        // Accessing ConcurrentDictionary.Values forces a collection snapshot (memory allocations)
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int IteratingDirectly()
    {
        // Iterating the ConcurrentDictionary directly via KeyValuePair avoids the snapshot overhead
        int count = 0;
        foreach (var kvp in _atms)
        {
            if (kvp.Value != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int IteratingYieldReturn()
    {
        // Using a yield return iterator to expose IEnumerable<Atm> without exposing KeyValuePair
        int count = 0;
        foreach (var atm in GetAtmsDirectly())
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerable<Atm> GetAtmsDirectly()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
