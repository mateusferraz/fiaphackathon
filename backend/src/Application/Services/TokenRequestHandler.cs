using Application.Interfaces;
using Application.Requests;
using Domain.Entidades;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class TokenRequestHandler : IRequestHandler<TokenRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TokenRequestHandler> _logger;
        private readonly ITokenService _tokenService;

        public TokenRequestHandler(IUnitOfWork unitOfWork, ILogger<TokenRequestHandler> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Autenticando Email do Medico: {request.Email}");

            Medico medico = null;
            Paciente paciente = null;

            if (request.TipoUsuario == TipoUsuario.Medico)
                medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Email == request.Email && x.Senha == request.Senha);
            else
                paciente = _unitOfWork.PacienteRepository.SelectOne(x => x.Email == request.Email && x.Senha == request.Senha);

            if (medico == null && paciente == null)
                throw new ArgumentException("Email ou senha Incorreta!");

            string documento = medico?.Documento ?? paciente.Documento;

            return _tokenService.GerarToken(documento, request.TipoUsuario);
        }
    }
}
