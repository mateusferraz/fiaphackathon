using Application.Interfaces;
using Application.Requests.AgendaPaciente;
using Domain.Entidades;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.AgendaPaciente
{
    public class AgendarPacienteRequestHandler : IRequestHandler<AgendarPacienteRequest, Unit>
    {
        private readonly ILogger<AgendarPacienteRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public AgendarPacienteRequestHandler(ILogger<AgendarPacienteRequestHandler> logger, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public Task<Unit> Handle(AgendarPacienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Realizando agendamento do paciente: {request.Documento}");

            var paciente = _unitOfWork.PacienteRepository.SelectOne(x => x.Documento == request.Documento)
                ?? throw new InvalidOperationException("Paciente não encontrado!");

            var agenda = _unitOfWork.AgendaRepository.SelectOne(x => x.Id == request.IdAgenda && x.Status == StatusAgendamento.Livre)
                ?? throw new InvalidOperationException("Agenda indisponivel!");

            _unitOfWork.AgendamentoRepository.Insert(new Agendamento
            {
                AgendaId = agenda.Id,
                PacienteId = paciente.Id
            });

            agenda.Status = StatusAgendamento.Agendado;

            _unitOfWork.AgendaRepository.Update(agenda);

            try
            {
                _emailService.EnviarEmail(paciente, agenda);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar email: {ex.Message}");

                throw new InvalidOperationException("Erro ao enviar email para o medico: {agenda.Medico.Email}");
            }
            return Unit.Task;
        }
    }
}
