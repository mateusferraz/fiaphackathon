using Application.Interfaces;
using Application.ViewMoldels;
using Domain.Entidades;
using MediatR;

namespace Application.Queries.Medicos
{
    public class ListarAgendaMedicoQuery : IRequest<IEnumerable<AgendaViewModel>>, IPersistable
    {
        public string Documento { get; set; }
    }
}