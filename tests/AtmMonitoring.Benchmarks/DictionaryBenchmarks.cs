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
                Location = $"Street {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark]
    public List<Atm> UseValuesProperty()
    {
        // Calling .Values on ConcurrentDictionary locks and creates a snapshot copy, which allocates heap memory.
        var list = new List<Atm>();
        foreach (var atm in _atms.Values)
        {
            list.Add(atm);
        }
        return list;
    }

    [Benchmark]
    public List<Atm> UseDirectIteration()
    {
        // Iterating over the ConcurrentDictionary directly avoids taking a snapshot copy, minimizing allocation.
        var list = new List<Atm>();
        foreach (var kvp in _atms)
        {
            list.Add(kvp.Value);
        }
        return list;
    }
}
