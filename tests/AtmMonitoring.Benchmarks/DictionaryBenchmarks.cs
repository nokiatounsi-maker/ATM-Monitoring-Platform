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
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 5000 + i,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public List<Atm> UseValues()
    {
        var list = new List<Atm>();
        foreach (var atm in _atms.Values)
        {
            list.Add(atm);
        }
        return list;
    }

    [Benchmark]
    public List<Atm> UseYieldReturn()
    {
        var list = new List<Atm>();
        foreach (var atm in GetAtmsIterative())
        {
            list.Add(atm);
        }
        return list;
    }

    private IEnumerable<Atm> GetAtmsIterative()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
