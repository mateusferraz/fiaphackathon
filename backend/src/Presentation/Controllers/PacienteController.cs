using Application.Queries.Paciente;
using Application.Requests.Pacientes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> BuscarMedico([FromQuery] string documento, [FromQuery] string nome, [FromQuery] string crm)
        {
            if(string.IsNullOrEmpty(documento) || string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(crm))
            {
                return BadRequest("");
            }
            //var authorizationResult = CheckDocumentClaim();
            //if (authorizationResult == null)
            //    return Unauthorized("Unauthorized user");

            try
            {
                return Ok(await mediator.Send(new GetMedicoQuery { Nome = nome, Documento = documento, Crm = crm }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("agendar")]
        public async Task<IActionResult> AgendarPaciente()
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
    }
}
