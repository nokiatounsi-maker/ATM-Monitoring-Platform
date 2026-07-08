namespace AtmMonitoring.Core;
public interface IAtmService { IEnumerable<Atm> GetAllAtms(); Atm? GetAtmById(string id); void UpdateAtmStatus(string id, AtmStatus status); }
public class AtmService : IAtmService {
    private readonly List<Atm> _atms = new() { new Atm { Id = "ATM001", Location = "Main Street", Status = AtmStatus.Online } };
    public IEnumerable<Atm> GetAllAtms() => _atms;
    public Atm? GetAtmById(string id) => _atms.FirstOrDefault(a => a.Id == id);
    public void UpdateAtmStatus(string id, AtmStatus status) { var atm = GetAtmById(id); if (atm != null) atm.Status = status; }
}
