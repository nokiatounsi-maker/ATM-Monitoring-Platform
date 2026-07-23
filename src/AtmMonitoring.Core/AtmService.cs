using System.Collections.Concurrent;

namespace AtmMonitoring.Core;

public interface IAtmService
{
    IEnumerable<Atm> GetAllAtms();
    Atm? GetAtmById(string id);
    void UpdateAtmStatus(string id, AtmStatus status);
}

/// <summary>
/// Service implementation for managing ATM monitoring data.
/// This class is marked as sealed to enable JIT devirtualization optimizations,
/// allowing the .NET runtime to inline methods and bypass virtual dispatch overhead.
/// </summary>
public sealed class AtmService : IAtmService
{
    // Using ConcurrentDictionary for O(1) lookup and thread-safety
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    public AtmService()
    {
        var initialAtm = new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online };
        _atms.TryAdd(initialAtm.Id, initialAtm);
    }

    /// <summary>
    /// Gets all ATMs.
    /// This is optimized to avoid calling _atms.Values, which creates a complete snapshot
    /// of the values collection and causes significant heap allocations (approx ~8 KB for 1000 items).
    /// Using direct dictionary iteration (KeyValuePair) and yield return avoids this allocation overhead.
    /// </summary>
    public IEnumerable<Atm> GetAllAtms()
    {
        // Avoid accessing .Values since it creates a collection snapshot.
        // Direct iteration over the ConcurrentDictionary avoids snapshot allocations.
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
