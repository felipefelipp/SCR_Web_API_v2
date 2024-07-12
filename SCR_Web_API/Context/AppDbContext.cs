
using Microsoft.EntityFrameworkCore;
using Models.Classificacao;
using Models.Cliente;
using Models.Funcionario;
using SCRO_Web_API.Models.Atendimento;
using SCRO_Web_API.Models.Classificacao;
using SCRO_Web_API.Models.Cliente;

namespace SCR_Web_API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Responsavel> Responsaveis { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pergunta> Perguntas { get; set; }
    public DbSet<CategoriaPergunta> CategoriaPerguntas { get; set; }
    public DbSet<PerguntaSelecionadaPaciente> PerguntaSelecionadaPaciente { get; set; }
    public DbSet<Resposta> Respostas { get; set; }
    public DbSet<RespostaSelecionadaPaciente> RespostaSelecionadaPaciente { get; set; }
    public DbSet<ClassificacaoPaciente> Classificacoes { get; set; }
    public DbSet<Resultado> Resultados { get; set; }
    public DbSet<AtendimentoPaciente> Atendimentos { get; set; }
    public DbSet<PacienteResponsavel> PacientesResponsaveis { get; set; }
}
