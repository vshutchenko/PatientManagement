namespace PatientManagement.Api.ViewModels.Patient;

/// <summary>
/// Represents the name details of a patient.
/// </summary>
public class PatientNameViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the patient's name.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the use of the name.
    /// </summary>
    public string Use { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the family (last) name of the patient.
    /// </summary>
    public string Family { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the given (first and middle) names of the patient.
    /// </summary>
    public List<string> Given { get; set; } = new();
}
