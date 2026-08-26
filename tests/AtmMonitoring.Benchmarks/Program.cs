using System.Text.Json;
using System.Text.Json.Serialization;
using AtmMonitoring.Api;
using AtmMonitoring.Core;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AtmMonitoring.Benchmarks;

[MemoryDiagnoser]
public class SerializationBenchmarks
{
    private Atm _atm = null!;
    private JsonSerializerOptions _reflectionOptions = null!;
    private JsonSerializerOptions _sourceGenOptions = null!;

    [GlobalSetup]
    public void Setup()
    {
        _atm = new Atm
        {
            Id = "ATM001",
            Location = "Main Street Branch",
            Status = AtmStatus.Online,
            CashBalance = 50000.00m,
            LastMaintenance = DateTime.UtcNow
        };

        _reflectionOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        _sourceGenOptions = new JsonSerializerOptions
        {
            TypeInfoResolver = AppJsonSerializerContext.Default
        };
    }

    [Benchmark(Baseline = true)]
    public string ReflectionSerialization()
    {
        return JsonSerializer.Serialize(_atm, _reflectionOptions);
    }

    [Benchmark]
    public string SourceGeneratedSerialization()
    {
        return JsonSerializer.Serialize(_atm, _sourceGenOptions);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<SerializationBenchmarks>();
    }
}
