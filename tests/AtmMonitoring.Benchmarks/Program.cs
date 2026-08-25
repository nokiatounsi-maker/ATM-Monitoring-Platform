using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Core;
using System.Collections.Concurrent;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private ConcurrentDictionary<string, Atm> _atms = new();

    [GlobalSetup]
    public void Setup()
    {
        _atms = new ConcurrentDictionary<string, Atm>();
        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online, CashBalance = 10000m, LastMaintenance = DateTime.UtcNow });
        }
    }

    [Benchmark(Baseline = true)]
    public int GetAllAtms_ValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count += atm.Id.Length;
        }
        return count;
    }

    [Benchmark]
    public int GetAllAtms_YieldReturn()
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

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<AtmServiceBenchmarks>();
    }
}
