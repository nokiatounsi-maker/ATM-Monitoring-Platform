using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
            var id = $"ATM{i:D4}";
            _atms.TryAdd(id, new Atm
            {
                Id = id,
                Location = $"Location {i}",
                Status = AtmStatus.Online,
                CashBalance = 10000m,
                LastMaintenance = DateTime.UtcNow
            });
        }
    }

    [Benchmark(Baseline = true)]
    public int IterationWithValuesProperty()
    {
        int count = 0;
        // Accessing .Values creates a snapshot collection allocation
        foreach (var atm in _atms.Values)
        {
            if (atm != null)
                count++;
        }
        return count;
    }

    [Benchmark]
    public int IterationWithDirectIteration()
    {
        int count = 0;
        // Direct iteration via pair.Value avoids snapshot allocation completely
        foreach (var pair in _atms)
        {
            if (pair.Value != null)
                count++;
        }
        return count;
    }
}
