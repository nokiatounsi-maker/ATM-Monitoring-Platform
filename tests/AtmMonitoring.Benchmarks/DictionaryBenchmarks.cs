using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class DictionaryBenchmarks
{
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    [GlobalSetup]
    public void Setup()
    {
        for (int i = 0; i < 1000; i++)
        {
            var atm = new Atm
            {
                Id = $"ATM{i:D3}",
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 50000m,
                LastMaintenance = DateTime.UtcNow
            };
            _atms.TryAdd(atm.Id, atm);
        }
    }

    [Benchmark(Baseline = true)]
    public int GetValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count += atm.Id.Length;
        }
        return count;
    }

    [Benchmark]
    public int GetValuesYield()
    {
        int count = 0;
        foreach (var atm in GetAtmsYield())
        {
            count += atm.Id.Length;
        }
        return count;
    }

    private IEnumerable<Atm> GetAtmsYield()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }
}
