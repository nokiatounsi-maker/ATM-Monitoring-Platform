using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();
    private readonly Consumer _consumer = new();

    [Params(10, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < ItemCount; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public void SnapshotValues()
    {
        var values = GetValuesViaSnapshot();
        values.Consume(_consumer);
    }

    [Benchmark]
    public void YieldReturnDirect()
    {
        var values = GetValuesViaYield();
        values.Consume(_consumer);
    }

    private IEnumerable<Atm> GetValuesViaSnapshot()
    {
        return _atms.Values;
    }

    private IEnumerable<Atm> GetValuesViaYield()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
