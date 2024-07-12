using SCR_Web_API.Repositories;
using SCR_Web_API.Repositories.Interfaces;
using SCR_Web_API.Repositories.UOW;
using SCR_Web_API.Repositories.UOW.Interfaces;

namespace SCR_Web_API.Configuration;

public static class ServiceRegistration
{ 
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();
    }
}
