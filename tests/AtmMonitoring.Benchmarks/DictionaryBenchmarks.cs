using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
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
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateWithValues()
    {
        // This accesses .Values, taking a snapshot of the dictionary and allocating memory.
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int IterateDirect()
    {
        // This iterates through the dictionary directly without taking a snapshot.
        int count = 0;
        foreach (var kvp in _atms)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int IterateWithYieldReturn()
    {
        // This simulates the yield return pattern we implemented in AtmService.GetAllAtms().
        int count = 0;
        foreach (var atm in GetAllAtmsYield())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> GetAllAtmsYield()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
