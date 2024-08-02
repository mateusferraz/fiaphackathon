using Application.Interfaces;
using Domain.Entidades;
using MediatR;

namespace Application.Requests.Medicos
{
    public class ListarAgendaMedicoQuery : IRequest<IEnumerable<Agenda>>, IPersistable
    {
        public string Documento { get ; set ; }
    }
}
