using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
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
            var id = $"ATM{i:D3}";
            _atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark]
    public int IterValues()
    {
        int count = 0;
        foreach (var atm in _atms.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int IterDirect()
    {
        int count = 0;
        foreach (var pair in _atms)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int IterYield()
    {
        int count = 0;
        foreach (var atm in GetAllAtmsYield())
        {
            count++;
        }
        return count;
    }

    private IEnumerable<Atm> GetAllAtmsYield()
    {
        foreach (var pair in _atms)
        {
            yield return pair.Value;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<DictionaryBenchmarks>();
    }
}
