using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Services.Infrastructure;
using FluentValidation;
using PatientManagement.Api.Validators;
using PatientManagement.Api.ViewModels.Patient;


namespace PatientManagement.Api.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, string connectionString)
    {
        services.AddServicesLayer(connectionString);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IValidator<PatientViewModel>, PatientViewModelValidator>();

        return services;
    }
}
