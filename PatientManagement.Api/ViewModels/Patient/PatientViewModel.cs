namespace PatientManagement.Api.ViewModels.Patient;

/// <summary>
/// Represents the patient information for creation and updates.
/// </summary>
public class PatientViewModel
{
    /// <summary>
    /// Gets or sets the name details of the patient.
    /// </summary>
    public PatientNameViewModel Name { get; set; } = new();

    /// <summary>
    /// Gets or sets the gender of the patient.
    /// Allowed values: "male", "female", "other", "unknown".
    /// </summary>
    public string Gender { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth date of the patient.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the patient is active.
    /// </summary>
    public bool Active { get; set; }
}
