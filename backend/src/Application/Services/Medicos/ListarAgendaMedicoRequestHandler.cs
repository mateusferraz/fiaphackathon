using Application.Interfaces;
using Application.Requests.Medicos;
using Domain.Common;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Accounts
{
    public class ListarAgendaMedicoRequestHandler : IRequestHandler<ListarAgendaMedicoQuery, IEnumerable<Agenda>>
    {
        private readonly ILogger<ListarAgendaMedicoRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ListarAgendaMedicoRequestHandler(ILogger<ListarAgendaMedicoRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Agenda>> Handle(ListarAgendaMedicoQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando agendas cadastradas para o Médico: {query.Documento}");

            var medico = _unitOfWork.MedicoRepository.SelectOne(x => x.Documento == query.Documento);
            
            if (medico == null)
                throw new InvalidOperationException("Medico não encontrado!");

            var agendas = _unitOfWork.AgendaRepository.Find();

            return agendas;
        }
    }
}
