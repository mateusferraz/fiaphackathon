using Application.Interfaces;
using Application.Requests.Medicos;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Application.Services.Accounts
{
    public class CadatrarAgendaMedicoRequestHandler : IRequestHandler<CadatrarAgendaMedicoRequest, Unit>
    {
        private readonly ILogger<CadatrarAgendaMedicoRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CadatrarAgendaMedicoRequestHandler(ILogger<CadatrarAgendaMedicoRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(CadatrarAgendaMedicoRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Cadastrando data disponivel agenda médico: {request.DataHoraDisponivel}");
            var medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Documento == request.MedicoDocumento);

            if (medico == null || medico.Id == Guid.Empty)
                throw new InvalidOperationException("Medico não está logado!");

            if (validarRequest(request, medico.Id)) {

               

                DateTime _dataAgendamento = DateTime.Parse(request.DataHoraDisponivel);
                var dataAgendamento = new Agenda
                {
                    DataAgendamento = _dataAgendamento,
                    MedicoId = medico.Id
                };

                _unitOfWork.AgendaRepository.Insert(dataAgendamento);
            }
            return Unit.Task;
        }       
        private bool validarRequest(CadatrarAgendaMedicoRequest request, Guid medicoId) {
            DateTime.TryParse(request.DataHoraDisponivel, out DateTime result);
            if(result == null || result == DateTime.MinValue)
                throw new InvalidOperationException("Formato de data inválida, por favor informe a data e hora no formato: \"dia/mês/ano hora:minuto\" ex:01/01/2000 12:00");

            string format = "dd/MM/yyyy HH:mm";
            
            if (!DateTime.TryParseExact(request.DataHoraDisponivel, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultDate))
                throw new InvalidOperationException("Formato de data inválida, por favor informe a data e hora no formato: \"dia/mês/ano hora:minuto\" ex:01/01/2000 12:00");

            DateTime _dataAgendamento = DateTime.Parse(request.DataHoraDisponivel);
            if (_dataAgendamento <= DateTime.Now)
                throw new InvalidOperationException("Data não pode ser menor que data atual!");

            var dataAgendadas = _unitOfWork.AgendaRepository.SelectOne(x => x.DataAgendamento == _dataAgendamento && x.Medico.Id == medicoId);

            if (dataAgendadas != null)
                throw new InvalidOperationException("Data já cadastrado!");


            return true;
        }
    }
}
