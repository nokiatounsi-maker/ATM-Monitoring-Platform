using BenchmarkDotNet.Attributes;
using System.Collections.Concurrent;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
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
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 5000.00m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    // Baseline: Accessing the .Values property, which allocates a snapshot array/collection under the hood.
    [Benchmark(Baseline = true)]
    public int UseValuesProperty()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count += atm.Id.Length;
        }
        return count;
    }

    // Optimization: Directly iterating over the ConcurrentDictionary to avoid allocating snapshot collections.
    [Benchmark]
    public int UseDirectIteration()
    {
        int count = 0;
        foreach (var kvp in _atms)
        {
            count += kvp.Value.Id.Length;
        }
        return count;
    }
}
