using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
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

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<GetAllAtmsBenchmark>();
    }
}
