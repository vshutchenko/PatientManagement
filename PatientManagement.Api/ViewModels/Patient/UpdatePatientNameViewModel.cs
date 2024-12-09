namespace PatientManagement.Api.ViewModels.Patient;

public class UpdatePatientNameViewModel
{
    public Guid Id { get; set; }
    public string Use { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public List<string> Given { get; set; } = new();
}
