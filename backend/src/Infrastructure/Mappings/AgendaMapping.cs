using Domain.Entidades;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class AgendaMapping : BaseMapping<Agenda>
    {
        protected override void MapEntity(EntityTypeBuilder<Agenda> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Agenda");

            entityTypeBuilder.Property(p => p.DataAgendamento)
                .IsRequired()
                .HasColumnName("data_agendamento");

            entityTypeBuilder.Property(p => p.Status)
                .IsRequired()
                .HasColumnName("status_agendamento");

            entityTypeBuilder.Property(p => p.Medico)
                .IsRequired()
                .HasColumnName("id_medico");

            entityTypeBuilder.HasAlternateKey(p => p.DataAgendamento);

            entityTypeBuilder.HasOne(b => b.AgendamentoPaciente)
                .WithOne(b => b.AgendaMedico)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
