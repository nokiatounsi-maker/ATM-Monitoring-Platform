using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

/// <summary>
/// Benchmark comparing memory allocations of ConcurrentDictionary .Values snapshot vs direct dictionary iteration.
/// </summary>
[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private ConcurrentDictionary<string, Atm> _dict = null!;

    [Params(10, 1000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _dict = new ConcurrentDictionary<string, Atm>();
        for (int i = 0; i < Size; i++)
        {
            var id = $"ATM{i:D6}";
            _dict.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark]
    public int IterDirectValues()
    {
        int count = 0;
        foreach (var atm in _dict.Values)
        {
            if (atm != null)
                count++;
        }
        return count;
    }

    [Benchmark]
    public int IterDirectDict()
    {
        int count = 0;
        foreach (var kvp in _dict)
        {
            if (kvp.Value != null)
                count++;
        }
        return count;
    }
}
