using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using patient.Models;

namespace patient.Service
{
  public class PatientService : IPatientService
  {
    private static List<Patient> _patients = new List<Patient>();
    private const string STORAGE_FILE = "patients.json";

    public PatientService()
    {
      LoadPatients();
    }

    private void LoadPatients()
    {
      if (System.IO.File.Exists(STORAGE_FILE))
      {
        string json = System.IO.File.ReadAllText(STORAGE_FILE);
        _patients = JsonSerializer.Deserialize<List<Patient>>(json) ?? new List<Patient>();
      }
    }

    private void SavePatients()
    {
      string json = JsonSerializer.Serialize(_patients);
      System.IO.File.WriteAllText(STORAGE_FILE, json);
    }

    public IEnumerable<Patient> GetPatients()
    {
      return _patients;
    }

    public Patient GetPatientById(int id)
    {
      return _patients.FirstOrDefault(p => p.Id == id);
    }

    public void AddPatient(Patient patient)
    {
      patient.Id = _patients.Count > 0 ? _patients.Max(p => p.Id) + 1 : 1;
      _patients.Add(patient);
      SavePatients();
    }

    public void UpdatePatient(Patient patient)
    {
      var existingPatient = _patients.FirstOrDefault(p => p.Id == patient.Id);
      if (existingPatient != null)
      {
        int index = _patients.IndexOf(existingPatient);
        _patients[index] = patient;
        SavePatients();
      }
    }

    public void DeletePatient(int id)
    {
      var patient = _patients.FirstOrDefault(p => p.Id == id);
      if (patient != null)
      {
        _patients.Remove(patient);
        SavePatients();
      }
    }
  }
}
