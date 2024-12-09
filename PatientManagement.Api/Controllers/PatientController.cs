using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Api.ViewModels.Patient;
using PatientManagement.Services.Patient;

namespace PatientManagement.Controllers;

[ApiController]
[Route("/patient")]
public class PatientController : ControllerBase
{
    private IPatientService _patientService;
    private IMapper _mapper;

    public PatientController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreatePatient(CreatePatientViewModel patientVM)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var patient = _mapper.Map<Patient>(patientVM);
        var createdPatient = _patientService.CreatePatient(patient);

        return CreatedAtAction(nameof(GetPatientById), new { id = createdPatient.Id }, createdPatient);
    }

    [HttpGet("{id}")]
    public IActionResult GetPatientById(Guid id)
    {
        var patient = _patientService.FindPatientById(id);

        if (patient == null)
        {
            return NotFound();
        }

        var patientVM = _mapper.Map<PatientViewModel>(patient);

        return Ok(patientVM);
    }

    [HttpGet("")]
    public IActionResult GetPatientByDate([FromQuery] string birthDate)
    {
        var searchParameter = SearchParameter.Parse(birthDate);
        var patients = _patientService.FindPatientsByDate(searchParameter);
        return Ok(patients);
    }

    [HttpPut("")]
    public IActionResult UpdatePatient(UpdatePatientViewModel patientVM)
    {
        var patientExists = _patientService.FindPatientById(patientVM.Name.Id) is not null;

        if (!patientExists)
        {
            return NotFound();
        }

        var patient = _mapper.Map<Patient>(patientVM);
        _patientService.UpdatePatient(patient);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePatient(Guid id)
    {
        _patientService.DeletePatient(id);

        return NoContent();
    }
}
