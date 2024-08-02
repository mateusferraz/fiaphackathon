using Domain.Entidades;
using Domain.Enums;

namespace Application.ViewMoldels
{
    public class AgendaViewModel
    {
        public Guid Id { get; set; }
        public string DataAgendamento { get; set; }
        public StatusAgendamento Status { get; set; }
    }

}
