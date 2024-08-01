using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Requests
{
    public class TokenRequest : IRequest<string>, IPersistable
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
