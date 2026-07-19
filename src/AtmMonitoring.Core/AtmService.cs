using System.Collections.Concurrent;

namespace AtmMonitoring.Core;

public interface IAtmService
{
    IEnumerable<Atm> GetAllAtms();
    Atm? GetAtmById(string id);
    void UpdateAtmStatus(string id, AtmStatus status);
}

/// <summary>
/// Service managing ATM states.
/// This class is sealed to enable JIT devirtualization optimizations.
/// Sealing concrete classes allows the JIT compiler to devirtualize interface calls
/// when it can prove there is only a single implementer, improving execution speed.
/// </summary>
public sealed class AtmService : IAtmService
{
    // Using ConcurrentDictionary for O(1) lookup and thread-safety.
    // Atm class is sealed to allow the compiler to perform devirtualization optimizations.
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    public AtmService()
    {
        var initialAtm = new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online };
        _atms.TryAdd(initialAtm.Id, initialAtm);
    }

    /// <summary>
    /// Enumerates the dictionary's values directly using a yield return.
    /// This avoids accessing .Values on ConcurrentDictionary, which is an O(N) operation
    /// that takes a snapshot of the dictionary and allocates a new collection on the heap.
    /// Direct iteration results in O(1) memory allocation overhead, avoiding unnecessary garbage collection.
    /// </summary>
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
