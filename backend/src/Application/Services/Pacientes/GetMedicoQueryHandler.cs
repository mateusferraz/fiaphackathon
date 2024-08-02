using Application.Interfaces;
using Application.Queries.Paciente;
using Application.ViewMoldels;
using AutoMapper;
using Domain.Common;
using Domain.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Application.Services.Pacientes
{
    public class GetMedicoQueryHandler : IRequestHandler<GetMedicoQuery, IEnumerable<MedicoViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetMedicoQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetMedicoQueryHandler(IUnitOfWork unitOfWork, ILogger<GetMedicoQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<MedicoViewModel>> Handle(GetMedicoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando o medico Documento: {request.Documento}");

            Expression<Func<Medico, bool>> filter = null;

            if (request != null && !string.IsNullOrWhiteSpace(request.Documento))
                filter = x => x.Documento == request.Documento;

            var queryResult = _unitOfWork.MedicoRepository.SelectMany(filter);

            if (queryResult == null)
                throw new NullReferenceException("Medico not found!");

            var mappedResult = _mapper.Map<IEnumerable<MedicoViewModel>>(queryResult);

            return Task.FromResult(mappedResult);
        }
    }
}
