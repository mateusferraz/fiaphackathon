using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class MedicoMapping : BaseMapping<Medico>
    {
        protected override void MapEntity(EntityTypeBuilder<Medico> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Medico");

            entityTypeBuilder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnName("nome");

            entityTypeBuilder.Property(p => p.Documento)
                .IsRequired()
                .HasColumnName("documento")
                .HasMaxLength(20);

            entityTypeBuilder.Property(p => p.Crm)
                .HasColumnName("crm");

            entityTypeBuilder.Property(p => p.Email)
                .IsRequired()
                .HasColumnName("email");

            entityTypeBuilder.Property(p => p.Senha)
                .IsRequired()
                .HasColumnName("senha")
                .HasMaxLength(50);

            entityTypeBuilder.HasAlternateKey(p => new { p.Documento, p.Crm });

            entityTypeBuilder.HasMany(b => b.Agendas)
                .WithOne(a => a.Medico)
                .HasForeignKey(a => a.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
