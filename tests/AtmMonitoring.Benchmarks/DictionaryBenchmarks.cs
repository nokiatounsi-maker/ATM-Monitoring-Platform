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
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public List<Atm> UseValuesSnapshot()
    {
        // Mimics accessing .Values (snapshot allocation)
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
        // Mimics our optimized direct yield return iteration
        var list = new List<Atm>();
        foreach (var pair in _atms)
        {
            list.Add(pair.Value);
        }
        return list;
    }
}
