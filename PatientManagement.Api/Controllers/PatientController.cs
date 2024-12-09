using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Api.Infrastructure;
using PatientManagement.Api.ViewModels.Patient;
using PatientManagement.Services.Patient;

namespace PatientManagement.Controllers;

[ApiController]
[Route("/patient")]
public class PatientController : ControllerBase
{
    private IPatientService _patientService;
    private IValidator<PatientViewModel> _validator;
    private IMapper _mapper;

    public PatientController(IPatientService patientService, IValidator<PatientViewModel> validator, IMapper mapper)
    {
        _patientService = patientService;
        _validator = validator;
        _mapper = mapper;
    }

    private bool IsPatientViewModelValid(PatientViewModel patientVM)
    {
        ValidationResult result = _validator.Validate(patientVM);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return false;
        }

        return true;
    }

    [HttpPost]
    public IActionResult CreatePatient(PatientViewModel patientVM)
    {
        if (!IsPatientViewModelValid(patientVM))
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

    [HttpGet]
    public IActionResult GetPatientByDate([FromQuery] string birthDate)
    {
        if (SearchParameter.TryParse(birthDate, out var searchParameter))
        {
            var patients = _patientService.FindPatientsByDate(searchParameter!);
            return Ok(patients);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public IActionResult UpdatePatient(PatientViewModel patientVM)
    {
        if (!IsPatientViewModelValid(patientVM))
        {
            return BadRequest(ModelState);
        }

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
