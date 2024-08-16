using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Cliente;
using Newtonsoft.Json;
using SCR_Web_API.DTO;
using SCR_Web_API.Filters;
using SCR_Web_API.Pagination;
using SCR_Web_API.Repositories.UOW.Interfaces;

namespace SCR_Web_API.Controllers;

[Route("[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    public PacienteController(IUnitOfWork unitOfWork, ILogger<PacienteController> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<PacienteDTO>> ObterPacientes([FromQuery] Parameters pacienteParameters)
    {
        _logger.LogInformation("-------------------- GET api/Paciente ------------------");

        var pacientes = _unitOfWork.PacienteRepository.GetAll(pacienteParameters);

        var metadata = new
        {
            pacientes.TotalCount,
            pacientes.PageSize,
            pacientes.CurrentPage,
            pacientes.TotalPages,
            pacientes.HasNext,
            pacientes.HasPrevious
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        if (pacientes == null)
            return NotFound($"Pacientes não encontrados");

        var pacientesDTO = _mapper.Map<IEnumerable<PacienteDTO>>(pacientes);
        
        return Ok(pacientesDTO);
    }

    [HttpGet("{id:int}", Name = "ObterPaciente")]
    public ActionResult<PacienteDTO> ObterPaciente(int id)
    {

        _logger.LogInformation($"-------------------- GET api/Paciente/id: {id} ------------------");

        var paciente = _unitOfWork.PacienteRepository.Get(p => p.PacienteId == id);

        if (paciente == null)
            return NotFound($"Paciente com id {id} não encontrado");

        var pacienteDTO = _mapper.Map<PacienteDTO>(paciente);

        return Ok(pacienteDTO);
    }

    [HttpPost]
    public ActionResult<PacienteDTO> CriarPaciente(PacienteDTO pacienteDto)
    {
        //TODO: Add funcionalidade para passar o id do paciente APENAS no response da requisição
        if (pacienteDto == null)
            return BadRequest($"Dados inválidos...");

        var paciente = _mapper.Map<Paciente>(pacienteDto);

        var pacienteCriado = _unitOfWork.PacienteRepository.Create(paciente);
        _unitOfWork.Commit();

        var pacienteCriadoDto = _mapper.Map<PacienteDTO>(pacienteCriado);

        return new CreatedAtRouteResult("ObterPaciente", new { id = pacienteCriadoDto.PacienteId }, pacienteCriadoDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PacienteDTO> AtualizarPaciente(int id, PacienteDTO pacienteDto)
    {
        if (id != pacienteDto.PacienteId)
            return BadRequest($"Id inválido");

        var paciente = _mapper.Map<Paciente>(pacienteDto);

        var pacienteAtualizado = _unitOfWork.PacienteRepository.Update(paciente);
        _unitOfWork.Commit();

        var pacienteAtualizadoDto = _mapper.Map<PacienteDTO>(pacienteAtualizado);

        return Ok(pacienteAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<PacienteDTO> DeletarPaciente(int id)
    {
        var paciente = _unitOfWork.PacienteRepository.Get(p => p.PacienteId == id);
        if (paciente == null)
            return BadRequest($"Paciente não encontrado!");

        var pacienteDeletado = _unitOfWork.PacienteRepository.Delete(paciente);
        _unitOfWork.Commit();

        var pacienteDeletadoDto = _mapper.Map<PacienteDTO>(pacienteDeletado); 

        return Ok(pacienteDeletadoDto);
    }
}
