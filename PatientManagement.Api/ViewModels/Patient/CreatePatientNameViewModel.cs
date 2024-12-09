namespace PatientManagement.Api.ViewModels.Patient;

public class CreatePatientNameViewModel
{
    public string Use { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public List<string> Given { get; set; } = new();
}
