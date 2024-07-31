using Application.Interfaces;
using MediatR;

namespace Application.Requests.Medicos
{
    public class CadatrarMedicoRequest : IRequest<Unit>, IPersistable
    {
        public string Nome { get; set; }
        public string Documento { get ; set ; }
        public string Crm { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
