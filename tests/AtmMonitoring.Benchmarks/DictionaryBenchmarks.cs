using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private ConcurrentDictionary<string, Atm> _atms = null!;

    [Params(10, 100, 1000)]
    public int ItemCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _atms = new ConcurrentDictionary<string, Atm>();
        for (int i = 0; i < ItemCount; i++)
        {
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000.00m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int ValuesSnapshot()
    {
        int count = 0;
        // Accessing .Values creates an O(N) copy/snapshot array or list under the hood.
        foreach (var atm in _atms.Values)
        {
            if (atm != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int YieldDirectIteration()
    {
        int count = 0;
        // Iterating the ConcurrentDictionary directly avoids creating a snapshot allocation.
        foreach (var kvp in _atms)
        {
            if (kvp.Value != null)
            {
                count++;
            }
        }
        return count;
    }
}
