using System.Collections.Concurrent;

namespace AtmMonitoring.Core;

public interface IAtmService
{
    IEnumerable<Atm> GetAllAtms();
    Atm? GetAtmById(string id);
    void UpdateAtmStatus(string id, AtmStatus status);
}

// Sealed class to enable JIT devirtualization optimizations
public sealed class AtmService : IAtmService
{
    // Using ConcurrentDictionary for O(1) lookup and thread-safety.
    // Note: We avoid using .Values in GetAllAtms() to prevent allocating a snapshot of the dictionary.
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    public AtmService()
    {
        var initialAtm = new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online };
        _atms.TryAdd(initialAtm.Id, initialAtm);
    }

    public IEnumerable<Atm> GetAllAtms()
    {
        // Use yield return to iterate over the dictionary directly.
        // This avoids the memory allocation and locking overhead of _atms.Values,
        // which creates a full snapshot of the dictionary values.
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
