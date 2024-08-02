using patient.Models;

namespace patient.Service
{
  public interface IPatientService
  {
    IEnumerable<Patient> GetPatients();
    Patient GetPatientById(int id);
    void AddPatient(Patient patient);
    void UpdatePatient(Patient patient);
    void DeletePatient(int id);
  }
}
