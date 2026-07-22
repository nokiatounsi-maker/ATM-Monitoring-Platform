using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int ValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int DirectIterationYield()
    {
        int count = 0;
        foreach (var atm in IterateDirectly())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> IterateDirectly()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
