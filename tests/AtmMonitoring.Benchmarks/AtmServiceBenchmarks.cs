using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Core;
using System.Collections.Concurrent;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _dictionary = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D3}";
            _dictionary.TryAdd(id, new Atm { Id = id, Location = "Location " + i });
        }
    }

    [Benchmark]
    public int Consume_ValuesProperty()
    {
        int count = 0;
        foreach (var value in _dictionary.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int Consume_Direct()
    {
        int count = 0;
        foreach (var kvp in _dictionary)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int Consume_YieldReturn()
    {
        int count = 0;
        foreach (var value in IterateValues(_dictionary))
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> IterateValues(ConcurrentDictionary<string, Atm> dict)
    {
        foreach (var kvp in dict)
        {
            yield return kvp.Value;
        }
    }
}
