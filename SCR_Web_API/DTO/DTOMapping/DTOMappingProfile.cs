using AutoMapper;
using Models.Cliente;

namespace SCR_Web_API.DTO.DTOMapping;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<Paciente, PacienteDTO>().ReverseMap();  
    }
}
