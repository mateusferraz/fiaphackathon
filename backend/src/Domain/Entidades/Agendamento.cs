using Domain.Entities;

namespace Domain.Entidades
{
    public class Agendamento : EntidadeBase
    {
        public Guid PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }
        public Guid AgendaId { get; set; }
        public virtual Agenda AgendaMedico { get; set; }
    }
}
