using Application.Interfaces;
using Domain.Entidades;
using MediatR;

namespace Application.Requests.Pacientes
{
    public class CadatrarPacienteRequest : IRequest<Unit>, IPersistable
    {
        public string Nome { get; set; }
        public string Documento { get ; set ; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
