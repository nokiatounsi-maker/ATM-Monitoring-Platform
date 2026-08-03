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
                Location = $"Street {i}",
                Status = AtmStatus.Online,
                CashBalance = 5000m,
                LastMaintenance = System.DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int IterateWithValuesSnapshot()
    {
        int count = 0;
        // Accessing .Values creates a snapshot array/list under the hood
        var values = _atms.Values;
        foreach (var item in values)
        {
            if (item != null)
            {
                count++;
            }
        }
        return count;
    }

    [Benchmark]
    public int IterateWithDirectYieldReturn()
    {
        int count = 0;
        // Directly iterating key-value pairs and yielding values avoids collection allocation
        foreach (var item in GetAllAtmsOptimized())
        {
            if (item != null)
            {
                count++;
            }
        }
        return count;
    }

    private IEnumerable<Atm> GetAllAtmsOptimized()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
