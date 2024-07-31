using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class PacienteMapping : BaseMapping<Paciente>
    {
        protected override void MapEntity(EntityTypeBuilder<Paciente> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Paciente");

            entityTypeBuilder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnName("nome");

            entityTypeBuilder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnName("documento")
                .HasMaxLength(20);

            entityTypeBuilder.Property(p => p.Email)
                .IsRequired()
                .HasColumnName("email");

            entityTypeBuilder.Property(p => p.Senha)
                .IsRequired()
                .HasColumnName("senha")
                .HasMaxLength(50);

            entityTypeBuilder.HasAlternateKey(p => p.Documento);

            entityTypeBuilder.HasMany(b => b.Agendamentos)
                .WithOne(a => a.Paciente)
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
