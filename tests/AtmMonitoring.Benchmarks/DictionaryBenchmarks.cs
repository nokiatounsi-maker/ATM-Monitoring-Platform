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
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateWithValuesSnapshot()
    {
        // This simulates accessing _atms.Values which allocates a snapshot collection
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            if (atm.Status == AtmStatus.Online)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int IterateDirectKeyValuePair()
    {
        // This simulates our optimized GetAllAtms() direct iteration of KeyValuePairs
        int count = 0;
        foreach (var kvp in _atms)
        {
            if (kvp.Value.Status == AtmStatus.Online)
            {
                count++;
            }
        }
        return count;
    }
}
