using Domain.Entidades;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class AgendamentoMapping : BaseMapping<Agendamento>
    {
        protected override void MapEntity(EntityTypeBuilder<Agendamento> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Agendamento");

            entityTypeBuilder.Property(p => p.PacienteId)
                .IsRequired()
                .HasColumnName("id_paciente");

            entityTypeBuilder.Property(p => p.AgendaId)
                .IsRequired()
                .HasColumnName("id_agenda");

            entityTypeBuilder.HasOne(a => a.Paciente)
                 .WithMany(p => p.Agendamentos)
                 .HasForeignKey(a => a.PacienteId)
                 .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder.HasOne(a => a.AgendaMedico)
                .WithOne(a => a.AgendamentoPaciente)
                .HasForeignKey<Agendamento>(a => a.AgendaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
