using Microsoft.EntityFrameworkCore;
using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.DataAccess.Database;
public class PatientDbContext : DbContext
{
    public PatientDbContext(DbContextOptions<PatientDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<PatientEntity> Patients { get; set; } = null!;
}
