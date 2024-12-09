﻿namespace PatientManagement.Api.ViewModels.Patient;

public class PatientViewModel
{
    public PatientNameViewModel Name { get; set; } = new();
    public string Gender { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public bool Active { get; set; }
}
