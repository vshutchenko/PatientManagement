using Microsoft.Extensions.DependencyInjection;
using PatientManagement.DataAccess.Infrastructure;
using PatientManagement.Services.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PatientManagement.Services.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDataAccessLayer(connectionString);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IPatientService, PatientService>();

        return services;
    }
}
