using Domain.Entidades;
using Domain.Entities;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new MedicoMapping().Initialize(modelBuilder.Entity<Medico>());
            new PacienteMapping().Initialize(modelBuilder.Entity<Paciente>());
            new AgendaMapping().Initialize(modelBuilder.Entity<Agenda>());
            new AgendamentoMapping().Initialize(modelBuilder.Entity<Agendamento>());
        }
    }
    
}
