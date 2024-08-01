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
        public async Task<IActionResult> CadastrarPaciente()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("login")]
        public async Task<IActionResult> Logar()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar-medico")]
        public async Task<IActionResult> BuscarMedico()
        {
            try
            {
                return Ok();
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
