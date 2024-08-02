using Domain.Entidades;
using Domain.Enums;

namespace Application.ViewMoldels
{
    public class MedicoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Crm { get; set; }
        public string Email { get; set; }
        public IEnumerable<AgendaViewModel> Agendas { get; set; }
    }

    public class AgendaViewModel
    {
        public Guid Id { get; set; }
        public DateTime DataAgendamento { get; set; }
        public StatusAgendamento Status { get; set; }
    }
}
