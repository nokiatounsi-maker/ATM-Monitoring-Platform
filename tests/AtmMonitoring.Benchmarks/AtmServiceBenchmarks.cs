using BenchmarkDotNet.Attributes;
using AtmMonitoring.Core;
using System.Collections.Concurrent;
using System.Reflection;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private AtmService _atmService = null!;
    private const int AtmCount = 1000;

    [GlobalSetup]
    public void Setup()
    {
        _atmService = new AtmService();
        var field = typeof(AtmService).GetField("_atms", BindingFlags.NonPublic | BindingFlags.Instance);
        var atms = (ConcurrentDictionary<string, Atm>)field!.GetValue(_atmService)!;

        for (int i = 1; i < AtmCount; i++)
        {
            var id = $"ATM{i:D3}";
            atms.TryAdd(id, new Atm { Id = id, Location = "Location " + i });
        }
    }

    [Benchmark]
    public int GetAllAtms_Optimized()
    {
        int count = 0;
        foreach (var atm in _atmService.GetAllAtms())
        {
            count++;
        }
        return count;
    }
}
