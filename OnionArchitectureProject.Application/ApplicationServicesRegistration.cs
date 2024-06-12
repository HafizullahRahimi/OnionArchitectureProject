using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureProject.Application.Services;
using System.Reflection;

namespace OnionArchitectureProject.Application;
public static class ApplicationServicesRegistration
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IService, Service>();
    }
}