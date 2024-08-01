using Application.Interfaces;
using Application.Requests.Medicos;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Accounts
{
    public class CadastrarMedicoRequestHandler : IRequestHandler<CadastrarMedicoRequest, Unit>
    {
        private readonly ILogger<CadastrarMedicoRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CadastrarMedicoRequestHandler(ILogger<CadastrarMedicoRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(CadastrarMedicoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cadastrando o Médico: {request.Documento}");

            var medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Documento == request.Documento);
            
            if (medico != null)
                throw new InvalidOperationException("Medico já cadastrado!");

            var emaillMedico = _unitOfWork.MedicoRepository.SelectOne(x => x.Email == request.Email);

            if (emaillMedico != null)
                throw new InvalidOperationException("E-mail Medico já cadastrado!");

            var crmMedico = _unitOfWork.MedicoRepository.SelectOne(x => x.Crm == request.Crm);
            if (crmMedico != null)
                throw new InvalidOperationException("CRM Medico já cadastrado!");

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
