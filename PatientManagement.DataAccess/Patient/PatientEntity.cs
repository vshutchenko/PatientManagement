using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.DataAccess.Patient;
public class PatientEntity
{
    public Guid Id { get; set; }
    public string? Use { get; set; }
    [Required]
    public string Family { get; set; } = string.Empty;
    public string? Given { get; set; }
    [MaxLength(7)]
    public string? Gender { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public bool? Active { get; set; }
}
