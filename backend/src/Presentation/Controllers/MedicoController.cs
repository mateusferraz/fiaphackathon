using Application.Queries.Medicos;
using Application.Requests.Medicos;
using Application.ViewMoldels;
using Domain.Entidades;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;

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

        [Authorize]
        [HttpPost("cadastrar-agenda")]
        public async Task<IActionResult> CadastrarAgenda([FromBody] CadatrarAgendaMedicoRequest data)
        {
            try
            {
                var authorizationResult = ClaimsHelper.CheckDocumentClaim(HttpContext.User);
                if (authorizationResult.tpUsuario != TipoUsuario.Medico)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new CadatrarAgendaMedicoRequest
                {
                    DataHoraDisponivel = data.DataHoraDisponivel,
                    MedicoDocumento = authorizationResult.documento
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("listar-agenda")]
        public async Task<ActionResult<IEnumerable<AgendaViewModel>>> EditarAgenda()
        {
            try
            {
                var authorizationResult = ClaimsHelper.CheckDocumentClaim(HttpContext.User);
                if (authorizationResult.tpUsuario != TipoUsuario.Medico)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new ListarAgendaMedicoQuery
                {
                    Documento = authorizationResult.documento
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [Authorize]
        [HttpPost("editar-agenda")]
        public async Task<IActionResult> EditarAgenda([FromBody] EditarAgendaMedicoRequest data)
        {
            try
            {
                var authorizationResult = ClaimsHelper.CheckDocumentClaim(HttpContext.User);
                if (authorizationResult.tpUsuario != TipoUsuario.Medico)
                    return Unauthorized("Usuário não autorizado.");

                return Ok(await mediator.Send(new EditarAgendaMedicoRequest
                {
                    idAgenda = data.idAgenda,
                    NovaDataHoraDisponivel = data.NovaDataHoraDisponivel,
                    MedicoDocumento = authorizationResult.documento
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
