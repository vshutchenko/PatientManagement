using PatientManagement.Services.Infrastructure;
using FluentValidation;
using PatientManagement.Api.Validators;
using PatientManagement.Api.ViewModels.Patient;
using System.Reflection;


namespace PatientManagement.Api.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, string connectionString)
    {
        services.AddServicesLayer(connectionString);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IValidator<PatientViewModel>, PatientViewModelValidator>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}
