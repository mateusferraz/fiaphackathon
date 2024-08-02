using Application.Queries.Medicos;
using Application.Requests.Medicos;
using Application.ViewMoldels;
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

        [HttpGet("listar-agenda")]
        public async Task<ActionResult<IEnumerable<AgendaViewModel>>> EditarAgenda()
        {
            try
            {
                var authorizationResult = CheckDocumentClaim();
                if (authorizationResult.Item1 == null)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new ListarAgendaMedicoQuery
                {
                    Documento = authorizationResult.Item1
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("editar-agenda")]
        public async Task<IActionResult> EditarAgenda([FromBody] EditarAgendaMedicoRequest data)
        {
            try
            {
                var authorizationResult = CheckDocumentClaim();
                if (authorizationResult.Item1 == null)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new EditarAgendaMedicoRequest
                {
                    idAgenda = data.idAgenda,
                    NovaDataHoraDisponivel = data.NovaDataHoraDisponivel,
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
