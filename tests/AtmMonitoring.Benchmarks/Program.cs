using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class ConcurrentDictBenchmark
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
    public int ValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count += atm.Id.Length;
        }
        return count;
    }

    [Benchmark]
    public int DirectYield()
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
        BenchmarkRunner.Run<ConcurrentDictBenchmark>();
    }
}
