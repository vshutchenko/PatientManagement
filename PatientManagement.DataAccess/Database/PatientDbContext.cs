using Microsoft.EntityFrameworkCore;
using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.DataAccess.Database;
internal class PatientDbContext : DbContext
{
    public DbSet<PatientEntity> Patients { get; set; } = null!;
}
