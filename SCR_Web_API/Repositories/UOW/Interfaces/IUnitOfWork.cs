using SCR_Web_API.Repositories.Interfaces;

namespace SCR_Web_API.Repositories.UOW.Interfaces;

public interface IUnitOfWork
{
    IPacienteRepository PacienteRepository { get; }
    Task Commit();
}
