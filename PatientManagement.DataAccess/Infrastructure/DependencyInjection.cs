using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;
using PatientManagement.DataAccess.Database;
using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PatientManagement.DataAccess.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PatientDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IPatientRepository, PatientRepository>();

        return services;
    }
}
