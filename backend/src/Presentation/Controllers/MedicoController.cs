using Application.Requests.Medicos;
using Domain.Entidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Presentation.Controllers
{
    public class MedicoController : StandardController
    {
        public MedicoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarMedico([FromBody] CadatrarMedicoRequest medico)
        {
            try
            {
                return Ok(await mediator.Send(new CadatrarMedicoRequest
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
        public async Task<IActionResult> CadastrarAgenda([FromBody] CadatrarAgendaMedicoRequest data)
        {
            try
            {
                //var authorizationResult = CheckAccountClaim();
                //if (authorizationResult == null)
                //    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new CadatrarAgendaMedicoRequest
                {
                    DataHoraDisponivel = data.DataHoraDisponivel,
                    MedicoId = Guid.Parse("DEC2E513-2678-4725-8D77-102D611BEF6E")
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private Guid CheckAccountClaim()
        {
            var user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return Guid.Empty;

            var documentClaim = user.Claims.FirstOrDefault(c => c.Type == "MedicoId");

            if (documentClaim == null)
                return Guid.Empty;

            if (Guid.TryParse(documentClaim.Value, out Guid medicoId))
                return medicoId;

            return Guid.Empty;
        }
    }
}
