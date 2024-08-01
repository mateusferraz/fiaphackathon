using Domain.Enums;

namespace Domain.Entidades
{
    public class Agenda : EntidadeBase
    {
        public DateTime DataAgendamento { get; set; }
        public StatusAgendamento Status { get; set; }
        public Guid MedicoId { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual Agendamento AgendamentoPaciente { get; set; }
    }
}
