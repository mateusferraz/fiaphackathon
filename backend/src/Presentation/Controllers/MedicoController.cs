using Application.Requests.Medicos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class MedicoController : StandardController
    {
        public MedicoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarMedico([FromBody] CadastrarMedicoRequest medico)
        {
            try
            {
                return Ok(await mediator.Send(new CadastrarMedicoRequest
                {
                    Documento = medico.Documento,
                    Nome = medico.Nome,
                    Crm = medico.Crm,
                    Email = medico.Email,
                    Senha = medico.Senha
                }));
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

        [HttpPost("cadastrar-agenda")]
        public async Task<IActionResult> CadastrarAgenda()
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
