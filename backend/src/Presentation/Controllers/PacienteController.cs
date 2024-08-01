using Application.Queries.Paciente;
using Application.Requests.Pacientes;
using Application.Requests.AgendaPaciente;
using MediatR;
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

        [HttpGet("buscar-medico")]
        public async Task<IActionResult> BuscarMedico([FromQuery] string documento)
        {
            //var authorizationResult = CheckDocumentClaim();
            //if (authorizationResult == null)
            //    return Unauthorized("Unauthorized user");

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
    }
}
