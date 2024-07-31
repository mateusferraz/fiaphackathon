using Application.Interfaces;
using Application.Requests.Medicos;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Accounts
{
    public class CadatrarMedicoRequestHandler : IRequestHandler<CadatrarMedicoRequest, Unit>
    {
        private readonly ILogger<CadatrarMedicoRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CadatrarMedicoRequestHandler(ILogger<CadatrarMedicoRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(CadatrarMedicoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cadastrando o cliente: {request.Documento}");

            var medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Documento == request.Documento);

            if (medico != null)
                throw new InvalidOperationException("Medico já cadastrado!");

            medico = new Medico
            {
                Documento = request.Documento,
                Nome = request.Nome,
                Crm = request.Crm,
                Email = request.Email,
                Senha = request.Senha,
            };

            _unitOfWork.MedicoRepository.Insert(medico);

            return Unit.Task;
        }
    }
}
