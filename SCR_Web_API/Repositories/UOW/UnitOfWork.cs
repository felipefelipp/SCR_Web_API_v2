using SCR_Web_API.Context;
using SCR_Web_API.Repositories.Interfaces;
using SCR_Web_API.Repositories.UOW.Interfaces;

namespace SCR_Web_API.Repositories.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPacienteRepository _pacienteRepository;

        public AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext context)
        {
            _appDbContext = context;
        }

        public IPacienteRepository PacienteRepository
        {
            get
            {
                return _pacienteRepository = _pacienteRepository ?? new PacienteRepository(_appDbContext);
            }
        }

        public async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _appDbContext.DisposeAsync();
        }
    }
}
