using Application.Interfaces;
using Application.Queries.Paciente;
using Application.ViewMoldels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Paciente
{
    public class GetMedicoQueryHandler : IRequestHandler<GetMedicoQuery, MedicoViewModel>
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

        public Task<MedicoViewModel> Handle(GetMedicoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando o medico Documento: {request.Nome}");

            var queryResult = _unitOfWork.MedicoRepository.Find(x => x.Documento == request.Documento
                                                                  || x.Crm == request.Crm
                                                                  || x.Nome == request.Nome);

            if (queryResult == null)
                throw new NullReferenceException("Medico not found!");

            var mappedResult = _mapper.Map<MedicoViewModel>(queryResult);

            return Task.FromResult(mappedResult);
        }
    }
}
