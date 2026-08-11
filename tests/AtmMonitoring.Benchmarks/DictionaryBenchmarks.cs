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
            var atm = new Atm
            {
                Id = $"ATM{i:D3}",
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000m,
                LastMaintenance = DateTime.UtcNow
            };
            _atms.TryAdd(atm.Id, atm);
        }
    }

    [Benchmark(Baseline = true)]
    public List<Atm> AccessValues()
    {
        // Accessing .Values creates a snapshot and copies elements, causing allocations
        var list = new List<Atm>();
        foreach (var atm in _atms.Values)
        {
            list.Add(atm);
        }
        return list;
    }

    [Benchmark]
    public List<Atm> DirectIteration()
    {
        // Iterating directly over the ConcurrentDictionary avoids creating the snapshot collection
        var list = new List<Atm>();
        foreach (var kvp in _atms)
        {
            list.Add(kvp.Value);
        }
        return list;
    }
}
