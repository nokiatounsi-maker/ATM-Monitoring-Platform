using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Core;

BenchmarkRunner.Run<AtmServiceBenchmarks>();

[MemoryDiagnoser]
public class AtmServiceBenchmarks
{
    private AtmService _service = null!;
    private ConcurrentDictionary<string, Atm> _dictionary = null!;

    [Params(100, 1000)]
    public int Count { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _service = new AtmService();
        _dictionary = new ConcurrentDictionary<string, Atm>();
        for (int i = 0; i < Count; i++)
        {
            var id = $"ATM{i:D4}";
            var atm = new Atm { Id = id, Location = $"Location {i}", Status = AtmStatus.Online, CashBalance = 1000m, LastMaintenance = DateTime.UtcNow };
            _dictionary.TryAdd(id, atm);
        }
    }

    [Benchmark(Baseline = true)]
    public int GetAllAtms_ValuesSnapshot()
    {
        int count = 0;
        foreach (var atm in _dictionary.Values)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int GetAllAtms_YieldReturnDirect()
    {
        int count = 0;
        foreach (var pair in _dictionary)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int GetAllAtms_ServiceMethod()
    {
        int count = 0;
        foreach (var atm in _service.GetAllAtms())
        {
            count++;
        }
        return count;
    }
}
