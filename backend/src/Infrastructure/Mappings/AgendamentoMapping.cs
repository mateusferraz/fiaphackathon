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

            entityTypeBuilder.HasOne(b => b.Paciente)
                .WithMany(a => a.Agendamentos)
                .HasForeignKey(a => a.AgendaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
