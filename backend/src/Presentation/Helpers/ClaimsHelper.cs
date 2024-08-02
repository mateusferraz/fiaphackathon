using Domain.Enums;
using System.Security.Claims;

namespace Presentation.Helpers
{
    public static class ClaimsHelper
    {
        public static (string documento, TipoUsuario? tpUsuario) CheckDocumentClaim(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return (null, null);

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Documento");
            var tipoUsuarioClaim = user.Claims.FirstOrDefault(c => c.Type == "TipoUsuario")?.Value;

            Enum.TryParse(tipoUsuarioClaim, out TipoUsuario tipoUsuario);

            if (documentClaim == null)
                return (null, null);

            return (documento: documentClaim.Value, tpUsuario: tipoUsuario);
        }
    }
}
