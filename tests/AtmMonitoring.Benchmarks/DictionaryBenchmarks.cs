using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(10, 100, 1000)]
    public int N { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < N; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public List<Atm> GetWithValues()
    {
        // Simulate iterating through values (which causes collection snapshot allocation)
        var list = new List<Atm>();
        foreach (var atm in _atms.Values)
        {
            list.Add(atm);
        }
        return list;
    }

    [Benchmark]
    public List<Atm> GetWithYield()
    {
        // Simulate iterating through KeyValuePairs directly (no collection snapshot allocation)
        var list = new List<Atm>();
        foreach (var kvp in _atms)
        {
            list.Add(kvp.Value);
        }
        return list;
    }
}
