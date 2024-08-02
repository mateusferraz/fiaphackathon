using Application.Interfaces;
using Application.Requests.AgendaPaciente;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.AgendaPaciente
{
    public class AgendarPacienteRequestHandler : IRequestHandler<AgendarPacienteRequest, Unit>
    {
        private readonly ILogger<AgendarPacienteRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public AgendarPacienteRequestHandler(ILogger<AgendarPacienteRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(AgendarPacienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Realizando agendamento do paciente: {request.Documento}");

            var paciente = _unitOfWork.PacienteRepository.SelectOne(x => x.Documento == request.Documento)
                ?? throw new InvalidOperationException("Paciente não encontrado!");

            var agenda = _unitOfWork.AgendaRepository.SelectOne(x => x.Id == request.IdAgenda)
                ?? throw new InvalidOperationException("Agenda indisponivel!");

            _unitOfWork.AgendamentoRepository.Insert(new Agendamento
            {
                AgendaId = agenda.Id,
                PacienteId = paciente.Id
            });

            return Unit.Task;
        }
    }
}
