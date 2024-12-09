using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Api.Infrastructure;
using PatientManagement.Api.ViewModels.Patient;
using PatientManagement.Services.Patient;

namespace PatientManagement.Controllers;

/// <summary>
/// Controller for managing patient records.
/// Provides endpoints for creating, retrieving, updating, and deleting patients.
/// </summary>
[ApiController]
[Route("/patient")]
public class PatientController : ControllerBase
{
    private IPatientService _patientService;
    private IValidator<PatientViewModel> _validator;
    private IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientController"/> class.
    /// </summary>
    /// <param name="patientService">The service responsible for patient-related operations.</param>
    /// <param name="validator">The validator for <see cref="PatientViewModel"/>.</param>
    /// <param name="mapper">The mapper for converting between models and entities.</param>
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

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patientVM">The patient view model for creation.</param>
    /// <returns>The created patient.</returns>
    /// <response code="201">Returns the created patient.</response>
    /// <response code="400">If the model validation fails.</response>
    [HttpPost]
    [ProducesResponseType(typeof(PatientViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Retrieves a patient by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the patient.</param>
    /// <returns>The patient view model.</returns>
    /// <response code="200">Returns the requested patient.</response>
    /// <response code="404">If the patient is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PatientViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Searches for patients by birth date with different conditions.
    /// </summary>
    /// <param name="birthDate">The birth date with a search condition.</param>
    /// <returns>A list of patients matching the condition.</returns>
    /// <response code="200">Returns the list of matching patients.</response>
    /// <response code="400">If the search parameter is invalid.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Updates an existing patient's information.
    /// </summary>
    /// <param name="patientVM">The patient view model for the update.</param>
    /// <returns>A result indicating the outcome of the operation.</returns>
    /// <response code="400">If the model validation fails.</response>
    /// <response code="404">If the patient does not exist.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Deletes a patient by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the patient.</param>
    /// <returns>A result indicating the outcome of the operation.</returns>
    /// <returns>No content if the deletion is successful.</returns>
    /// <response code="204">The patient was successfully deleted.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletePatient(Guid id)
    {
        _patientService.DeletePatient(id);

        return NoContent();
    }
}
