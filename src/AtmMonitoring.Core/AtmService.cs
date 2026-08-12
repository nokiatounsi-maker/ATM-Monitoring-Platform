using System.Collections.Concurrent;

namespace AtmMonitoring.Core;

public interface IAtmService
{
    IEnumerable<Atm> GetAllAtms();
    Atm? GetAtmById(string id);
    void UpdateAtmStatus(string id, AtmStatus status);
}

// Sealed class to enable JIT devirtualization optimizations.
public sealed class AtmService : IAtmService
{
    // Using ConcurrentDictionary for O(1) lookup and thread-safety
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    public AtmService()
    {
        var initialAtm = new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online };
        _atms.TryAdd(initialAtm.Id, initialAtm);
    }

    // Optimization: Accessing .Values on a ConcurrentDictionary takes a full collection copy snapshot
    // under the hood, which is highly memory-inefficient and causes heap allocations.
    // By iterating directly over the KeyValuePair entries of the ConcurrentDictionary via yield return,
    // we bypass the snapshot creation completely, saving memory and avoiding garbage collection overhead.
    public IEnumerable<Atm> GetAllAtms()
    {
        foreach (var kvp in _atms)
        {
            yield return kvp.Value;
        }
    }

    public Atm? GetAtmById(string id) => _atms.TryGetValue(id, out var atm) ? atm : null;

    public void UpdateAtmStatus(string id, AtmStatus status)
    {
        if (_atms.TryGetValue(id, out var atm))
        {
            atm.Status = status;
        }
    }
}
