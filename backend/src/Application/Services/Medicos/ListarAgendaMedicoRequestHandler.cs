using Application.Interfaces;
using Application.Queries.Medicos;
using Application.ViewMoldels;
using Domain.Common;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Accounts
{
    public class ListarAgendaMedicoRequestHandler : IRequestHandler<ListarAgendaMedicoQuery, IEnumerable<AgendaViewModel>>
    {
        private readonly ILogger<ListarAgendaMedicoRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ListarAgendaMedicoRequestHandler(ILogger<ListarAgendaMedicoRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AgendaViewModel>> Handle(ListarAgendaMedicoQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando agendas cadastradas para o Médico: {query.Documento}");

            var medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Documento == query.Documento);
            
            if (medico == null)
                throw new InvalidOperationException("Medico não encontrado!");

            var agendas = _unitOfWork.AgendaRepository.Find(x => x.MedicoId == medico.Id);
            var agandaData = new List<AgendaViewModel>();
            foreach (var agenda in agendas)
            {
                agandaData.Add(new AgendaViewModel
                {
                    DataAgendamento = agenda.DataAgendamento.ToString("dd/MM/yyyy HH:mm"),
                    Id = agenda.Id,
                    Status = agenda.Status
                });
            }

            return agandaData;
        }
    }
}
