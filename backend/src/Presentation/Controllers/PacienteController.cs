using Application.Queries.Paciente;
using Application.Requests.Pacientes;
using Domain.Enums;
using Application.Requests.AgendaPaciente;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;

namespace Presentation.Controllers
{
    public class PacienteController : StandardController
    {
        public PacienteController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarPaciente([FromBody] CadatrarPacienteRequest paciente)
        {
            try
            {
                return Ok(await mediator.Send(paciente));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("buscar-medico")]
        public async Task<IActionResult> BuscarMedico([FromQuery] string documento)
        {
            var authorizationResult = CheckDocumentClaim();
            if (authorizationResult.tpUsuario != TipoUsuario.Paciente )
                return Unauthorized("Usuário não autorizado.");

            try
            {
                return Ok(await mediator.Send(new GetMedicoQuery {Documento = documento}));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("agendar")]
        public async Task<IActionResult> AgendarPaciente(Guid idAgenda)
        {
            try
            {
                var authorizationResult = ClaimsHelper.CheckDocumentClaim(HttpContext.User);
                if (authorizationResult.Item1 == null)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new AgendarPacienteRequest
                {
                    Documento = authorizationResult.Item1,
                    IdAgenda = idAgenda
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private (string token, TipoUsuario? tpUsuario) CheckDocumentClaim()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return (null, null);

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "Documento");
            var tipoUsuarioClaim = user.Claims.FirstOrDefault(c => c.Type == "TipoUsuario").Value;

            Enum.TryParse(tipoUsuarioClaim, out TipoUsuario tipoUsuario);

            if (documentClaim == null)
                return (null, null);

            return (token: documentClaim.Value,tpUsuario: tipoUsuario);
        }
    }
}
