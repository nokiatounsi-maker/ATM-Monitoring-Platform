using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, string> _dict = new();

    [Params(100, 1000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < N; i++)
        {
            _dict.TryAdd($"KEY{i}", $"VALUE{i}");
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateValuesOnly()
    {
        int count = 0;
        foreach (var val in _dict.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int IterateYieldOnly()
    {
        int count = 0;
        foreach (var val in GetValuesYield())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<string> GetValuesYield()
    {
        foreach (var kvp in _dict)
        {
            yield return kvp.Value;
        }
    }
}
