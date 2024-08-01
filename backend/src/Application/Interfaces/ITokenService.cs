using Domain.Enums;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(string documento, TipoUsuario tipoUsuario);
    }
}
