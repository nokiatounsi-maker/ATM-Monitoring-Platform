using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using AtmMonitoring.Api;
using AtmMonitoring.Core;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class GetAllAtmsBenchmark
{
    private AtmService _service = null!;

    [GlobalSetup]
    public void Setup()
    {
        _service = new AtmService();
    }

    [Benchmark]
    public int GetAllAtmsIteration()
    {
        int count = 0;
        foreach (var atm in _service.GetAllAtms())
        {
            count++;
        }
        return count;
    }
}

[MemoryDiagnoser]
public class JsonSerializationBenchmark
{
    private List<Atm> _atms = null!;

    [GlobalSetup]
    public void Setup()
    {
        _atms = new List<Atm>();
        for (int i = 0; i < 100; i++)
        {
            _atms.Add(new Atm
            {
                Id = $"ATM{i:D3}",
                Location = $"Location {i}",
                Status = (AtmStatus)(i % 4),
                CashBalance = 1000m * i,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public string ReflectionSerialization()
    {
        return JsonSerializer.Serialize(_atms);
    }

    [Benchmark]
    public string SourceGeneratedSerialization()
    {
        return JsonSerializer.Serialize(_atms, AppJsonSerializerContext.Default.ListAtm);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<GetAllAtmsBenchmark>();
        BenchmarkRunner.Run<JsonSerializationBenchmark>();
    }
}
