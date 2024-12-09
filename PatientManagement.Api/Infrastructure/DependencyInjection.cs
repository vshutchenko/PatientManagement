using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Services.Infrastructure;

namespace PatientManagement.Api.Infrastructure;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiLevel(this IServiceCollection services, string connectionString)
    {
        services.AddServicesLayer(connectionString);
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
