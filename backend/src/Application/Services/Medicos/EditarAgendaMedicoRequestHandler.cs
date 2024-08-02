using Application.Interfaces;
using Application.Requests.Medicos;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Application.Services.Accounts
{
    public class EditarAgendaMedicoRequestHandler : IRequestHandler<EditarAgendaMedicoRequest, Unit>
    {
        private readonly ILogger<EditarAgendaMedicoRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public EditarAgendaMedicoRequestHandler(ILogger<EditarAgendaMedicoRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(EditarAgendaMedicoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Editando registro da agenda do Médico: {request.MedicoDocumento}, Id do registro: {request.idAgenda}");

            var medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Documento == request.MedicoDocumento);
            
            if (medico == null)
                throw new InvalidOperationException("Medico não encontrado!");

            if (RequestValidada(request, medico.Id))
            {                
                var agenda = medico.Agendas.FirstOrDefault(x => x.Id == request.idAgenda);

                if (agenda == null)
                    throw new InvalidOperationException("Agenda não encontrada!");

                var agendamentoPaciente = _unitOfWork.AgendamentoRepository.SelectOne(x => x.AgendaId == agenda.Id);
                if (agendamentoPaciente != null)
                    throw new InvalidOperationException("Este registro não pode ser alterado pois já está agendado para um paciente!");

                var newAgenda = new Agenda
                {
                    DataAgendamento = DateTime.Parse(request.NovaDataHoraDisponivel),
                    MedicoId = agenda.MedicoId,
                };
                 
                _unitOfWork.AgendaRepository.Delete(agenda);
                _unitOfWork.AgendaRepository.Insert(newAgenda);
            }
            return Unit.Task;
        }

        private bool RequestValidada(EditarAgendaMedicoRequest request, Guid medicoId)
        {
            DateTime.TryParse(request.NovaDataHoraDisponivel, out DateTime result);
            if (result == null || result == DateTime.MinValue)
                throw new InvalidOperationException("Formato de data inválida, por favor informe a data e hora no formato: \"dia/mês/ano hora:minuto\" ex:01/01/2000 12:00");

            string format = "dd/MM/yyyy HH:mm";

            if (!DateTime.TryParseExact(request.NovaDataHoraDisponivel, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultDate))
                throw new InvalidOperationException("Formato de data inválida, por favor informe a data e hora no formato: \"dia/mês/ano hora:minuto\" ex:01/01/2000 12:00");

            DateTime _dataAgendamento = DateTime.Parse(request.NovaDataHoraDisponivel);
            if (_dataAgendamento <= DateTime.Now)
                throw new InvalidOperationException("Data não pode ser menor que data atual!");

            var dataAgendadas = _unitOfWork.AgendaRepository.SelectOne(x => x.DataAgendamento == _dataAgendamento && x.Medico.Id == medicoId);

            if (dataAgendadas != null)
                throw new InvalidOperationException("Data já cadastrada!");


            return true;
        }
    }
}
