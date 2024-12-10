using Microsoft.Extensions.DependencyInjection;
using PatientManagement.DataAccess.Infrastructure;
using PatientManagement.Services.Patient;
using PatientManagement.Services.Parsers;


namespace PatientManagement.Services.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services, string connectionString)
    {
        services.AddDataAccessLayer(connectionString);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IPatientService, PatientService>();
        services.AddTransient<IPatientExpressionParser, PatientExpressionParser>();
        return services;
    }
}
