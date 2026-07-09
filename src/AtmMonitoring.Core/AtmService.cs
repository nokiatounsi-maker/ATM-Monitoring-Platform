using System.Collections.Concurrent;

namespace AtmMonitoring.Core;

public interface IAtmService
{
    IEnumerable<Atm> GetAllAtms();
    Atm? GetAtmById(string id);
    void UpdateAtmStatus(string id, AtmStatus status);
}

public class AtmService : IAtmService
{
    // Using ConcurrentDictionary for O(1) lookup and thread-safety
    private readonly ConcurrentDictionary<string, Atm> _atms = new();

    public AtmService()
    {
        var initialAtm = new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online };
        _atms.TryAdd(initialAtm.Id, initialAtm);
    }

    public IEnumerable<Atm> GetAllAtms() => _atms.Values;

    public Atm? GetAtmById(string id) => _atms.TryGetValue(id, out var atm) ? atm : null;

    public void UpdateAtmStatus(string id, AtmStatus status)
    {
        if (_atms.TryGetValue(id, out var atm))
        {
            atm.Status = status;
        }
    }
}
