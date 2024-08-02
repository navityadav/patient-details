using Microsoft.AspNetCore.Mvc;
using patient.Models;
using patient.Service;
using System;
using System.Collections.Generic;

namespace patient.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PatientsController : ControllerBase
  {
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
      _patientService = patientService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Patient>> Get()
    {
      var patients = _patientService.GetPatients();
      return Ok(patients);
    }

    [HttpPost]
    public ActionResult<Patient> Create(Patient patient)
    {
      if (patient == null)
      {
        return BadRequest("Patient data is required.");
      }

      try
      {
        _patientService.AddPatient(patient);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
      }
      catch (Exception ex)
      {
        // Log the exception (ex)
        return StatusCode(500, $"An error occurred while creating the patient: {ex.Message}");
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Patient> GetById(int id)
    {
      var patient = _patientService.GetPatientById(id);
      if (patient == null)
      {
        return NotFound($"Patient with ID {id} not found.");
      }
      return Ok(patient);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Patient patient)
    {
      if (id != patient.Id)
      {
        return BadRequest("ID in the URL does not match the ID in the request body.");
      }

      var existingPatient = _patientService.GetPatientById(id);
      if (existingPatient == null)
      {
        return NotFound($"Patient with ID {id} not found.");
      }

      try
      {
        _patientService.UpdatePatient(patient);
        return NoContent();
      }
      catch (Exception ex)
      {
        // Log the exception (ex)
        return StatusCode(500, $"An error occurred while updating the patient: {ex.Message}");
      }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var patient = _patientService.GetPatientById(id);
      if (patient == null)
      {
        return NotFound($"Patient with ID {id} not found.");
      }

      try
      {
        _patientService.DeletePatient(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        // Log the exception (ex)
        return StatusCode(500, $"An error occurred while deleting the patient: {ex.Message}");
      }
    }
  }
}
