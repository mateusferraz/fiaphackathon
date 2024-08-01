using Application.Model;
using Application.Requests;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class LoginController : StandardController
    {
        public LoginController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("medico")]
        public async Task<ActionResult<string>> AutenticarMedico([FromQuery] LoginModel tokenDto)
        {
            try
            {
                var token =  await mediator.Send(new TokenRequest
                {
                    Email = tokenDto.Email,
                    Senha = tokenDto.Senha,
                    TipoUsuario = TipoUsuario.Medico
                });

                return Ok("{\"token\": \""+token+"\"}");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("paciente")]
        public async Task<ActionResult<string>> AutenticarPaciente([FromQuery] LoginModel tokenDto)
        {
            try
            {
                var token = await mediator.Send(new TokenRequest
                {
                    Email = tokenDto.Email,
                    Senha = tokenDto.Senha,
                    TipoUsuario = TipoUsuario.Paciente
                });

                return Ok("{\"token\": \"" + token + "\"}");
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        private (string, TipoUsuario?) CheckDocumentClaim()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return (null, null);

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Documento");
            var tipoUsuarioClaim = user.Claims.FirstOrDefault(c => c.Type == "TipoUsuario").Value;

            Enum.TryParse(tipoUsuarioClaim, out TipoUsuario tipoUsuario);

            if (documentClaim == null)
                return (null, null);

            return (documentClaim.Value, tipoUsuario);
        }
    }
}