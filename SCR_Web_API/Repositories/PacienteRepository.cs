using Models.Cliente;
using SCR_Web_API.Context;
using SCR_Web_API.Repositories.Interfaces;

namespace SCR_Web_API.Repositories;

public class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
