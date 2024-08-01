using Application.Requests.Medicos;
using Domain.Entidades;
using Domain.Enums;
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
        public async Task<IActionResult> CadastrarAgenda([FromBody] CadatrarAgendaMedicoRequest data)
        {
            try
            {
                var authorizationResult = CheckDocumentClaim();
                if (authorizationResult.Item1 == null)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new CadatrarAgendaMedicoRequest
                {
                    DataHoraDisponivel = data.DataHoraDisponivel,
                    MedicoDocumento = authorizationResult.Item1
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
