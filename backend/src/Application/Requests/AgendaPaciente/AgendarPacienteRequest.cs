using Application.Interfaces;
using MediatR;

namespace Application.Requests.AgendaPaciente
{
    public class AgendarPacienteRequest : IRequest<Unit>, IPersistable
    {
        public string Documento { get; set; }
        public Guid IdAgenda { get; set; }
    }
}
