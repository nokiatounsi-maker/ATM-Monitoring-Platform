using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

/// <summary>
/// Benchmark to analyze the memory and execution performance of accessing
/// ConcurrentDictionary.Values vs direct iteration using yield return.
/// </summary>
[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [Params(10, 100, 1000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms.Clear();
        for (int i = 0; i < Size; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 5000m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateValuesProperty()
    {
        int count = 0;
        // Accessing .Values creates a snapshot array/collection under the hood,
        // resulting in O(N) heap allocations.
        foreach (var atm in _atms.Values)
        {
            if (atm.Status == AtmStatus.Online)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int IterateDirectWithYieldReturn()
    {
        int count = 0;
        // Iterating directly over the ConcurrentDictionary avoids the snapshot allocation
        // of .Values, improving memory efficiency.
        foreach (var atm in GetAtmsDirect())
        {
            if (atm.Status == AtmStatus.Online)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerable<Atm> GetAtmsDirect()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
