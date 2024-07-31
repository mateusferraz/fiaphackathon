﻿using Domain.Entidades;
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

            entityTypeBuilder.Property(p => p.MedicoId)
                .IsRequired()
                .HasColumnName("id_medico");

            entityTypeBuilder.Property(p => p.AgendamentoId)
               .HasColumnName("id_agendamento");

            entityTypeBuilder.HasAlternateKey(p => p.DataAgendamento);

            entityTypeBuilder.HasOne(a => a.Medico)
                .WithMany(m => m.Agendas)
                .HasForeignKey(a => a.MedicoId)
                .OnDelete(DeleteBehavior.Cascade);

            entityTypeBuilder.HasOne(a => a.AgendamentoPaciente)
                .WithOne(a => a.AgendaMedico)
                .HasForeignKey<Agenda>(a => a.AgendamentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}