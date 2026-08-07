using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int UseValuesProperty()
    {
        int count = 0;
        // Accessing .Values creates a copy/snapshot allocation
        var values = _atms.Values;
        foreach (var atm in values)
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int UseDirectIteration()
    {
        int count = 0;
        // Direct iteration avoids snapshot allocation
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
    public int UseYieldReturn()
    {
        int count = 0;
        // Yield return implementation similar to AtmService.GetAllAtms()
        foreach (var atm in IterateDirect())
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerable<Atm> IterateDirect()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
