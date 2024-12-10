namespace PatientManagement.Services.Patient;
public class Patient
{
    public Guid Id { get; set; }
    public string Use { get; set; } = string.Empty;
    public string Family { get; set; } = string.Empty;
    public List<string> Given { get; set; } = new List<string>();
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}
