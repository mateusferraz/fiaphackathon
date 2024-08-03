using Application.Requests.Pacientes;
using Domain.Enums;
using Application.Requests.AgendaPaciente;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Application.Queries.Pacientes;

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
        [HttpGet("buscar-agenda-medico")]
        public async Task<IActionResult> BuscarMedico()
        {
            var authorizationResult = ClaimsHelper.CheckDocumentClaim(HttpContext.User);
            if (authorizationResult.tpUsuario != TipoUsuario.Paciente )
                return Unauthorized("Usuário não autorizado.");

            try
            {
                return Ok(await mediator.Send(new GetMedicoQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("agendar")]
        public async Task<IActionResult> AgendarPaciente(Guid idAgenda)
        {
            try
            {
                var authorizationResult = ClaimsHelper.CheckDocumentClaim(HttpContext.User);
                if (authorizationResult.documento == null)
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
    }
}
