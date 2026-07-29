using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(1000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < Count; i++)
        {
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int GetAllAtms_Snapshot()
    {
        // Using .Values which allocates a snapshot collection
        var count = 0;
        foreach (var atm in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int GetAllAtms_YieldReturn()
    {
        // Direct iteration using yield return (no snapshot)
        var count = 0;
        foreach (var atm in IterateAtmsDirectly())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> IterateAtmsDirectly()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
