using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

/// <summary>
/// Benchmark to analyze and compare memory and execution time overhead of ConcurrentDictionary .Values vs direct iteration.
/// </summary>
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
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark(Baseline = true)]
    public int ValuesPropertySnapshot()
    {
        int count = 0;
        // This forces ConcurrentDictionary to create a snapshot list of values, allocating memory
        foreach (var atm in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int DirectIteration()
    {
        int count = 0;
        // This iterates the KeyValuePairs directly without snapshotting
        foreach (var kvp in _atms)
        {
            count++;
        }
        return count;
    }
}
