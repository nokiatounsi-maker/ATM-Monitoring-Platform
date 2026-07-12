using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Core;
using System.Collections.Concurrent;
using System.Reflection;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private readonly AtmService _atmService = new();

    [GlobalSetup]
    public void Setup()
    {
        var field = typeof(AtmService).GetField("_atms", BindingFlags.NonPublic | BindingFlags.Instance);
        var atms = (ConcurrentDictionary<string, Atm>)field.GetValue(_atmService);

        for (int i = 0; i < 1000; i++)
        {
            var id = $"ATM{i:D3}";
            atms.TryAdd(id, new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online });
        }
    }

    [Benchmark]
    public int GetAllAtms_Iteration()
    {
        int count = 0;
        foreach (var atm in _atmService.GetAllAtms())
        {
            count++;
        }
        return count;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<AtmServiceBenchmarks>();
    }
}
