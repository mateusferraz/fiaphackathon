using Application.Interfaces;
using Application.Requests.Pacientes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Paciente
{
    public class CadastrarPacienteRequestHandler : IRequestHandler<CadatrarPacienteRequest, Unit>
    {
        private readonly ILogger<CadastrarPacienteRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CadastrarPacienteRequestHandler(ILogger<CadastrarPacienteRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(CadatrarPacienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cadastrando o cliente: {request.Documento}");

            var paciente = _unitOfWork.PacienteRepository.SelectOne(x => x.Documento == request.Documento);

            if (paciente != null)
                throw new InvalidOperationException("Paciente já cadastrado!");

            paciente = new Domain.Entities.Paciente
            {
                Documento = request.Documento,
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha,
                DataCriacao = DateTime.Now,
            };

            _unitOfWork.PacienteRepository.Insert(paciente);

            return Unit.Task;
        }
    }
}
