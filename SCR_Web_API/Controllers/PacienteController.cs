﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Cliente;
using SCR_Web_API.Filters;
using SCR_Web_API.Repositories.UOW.Interfaces;

namespace SCR_Web_API.Controllers;

[Route("[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public PacienteController(IUnitOfWork unitOfWork, ILogger<PacienteController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Paciente>> ObterPacientes()
    {
        _logger.LogInformation("-------------------- GET api/Paciente ------------------");

        var pacientes = _unitOfWork.PacienteRepository.GetAll();
        if (pacientes == null)
        {
            return NotFound($"Pacientes não encontrados");
        }
        return Ok(pacientes);
    }

    [HttpGet("{id:int}", Name = "ObterPaciente")]
    public ActionResult<Paciente> ObterPaciente(int id)
    {

        _logger.LogInformation($"-------------------- GET api/Paciente/id: {id} ------------------");

        var paciente = _unitOfWork.PacienteRepository.Get(p => p.PacienteId == id);

        if (paciente == null)
            return NotFound($"Paciente com id {id} não encontrado");
        return Ok(paciente);
    }

    [HttpPost]
    public ActionResult CriarPaciente(Paciente paciente)
    {
        if (paciente == null)
            return BadRequest($"Dados inválidos...");

        var pacienteCriado = _unitOfWork.PacienteRepository.Create(paciente);
        _unitOfWork.Commit();

        return new CreatedAtRouteResult("ObterPaciente", new { id = paciente.PacienteId }, paciente);
    }

    [HttpPut("{id:int}")]
    public ActionResult AtualizarPaciente(int id, Paciente paciente)
    {
        if (id != paciente.PacienteId)
            return BadRequest($"Id inválido");

        var pacienteAtualizado = _unitOfWork.PacienteRepository.Update(paciente);
        _unitOfWork.Commit();

        return Ok(pacienteAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeletarPaciente(int id)
    {
        var paciente = _unitOfWork.PacienteRepository.Get(p => p.PacienteId == id);
        if (paciente == null)
            return BadRequest($"Paciente não encontrado!");

        var pacienteDeletado = _unitOfWork.PacienteRepository.Delete(paciente);
        _unitOfWork.Commit();

        return Ok(pacienteDeletado);
    }
}
