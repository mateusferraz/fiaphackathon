﻿using Application.Interfaces;
using Application.Queries.Pacientes;
using Application.ViewMoldels;
using AutoMapper;
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

        public async Task<IEnumerable<MedicoViewModel>> Handle(GetMedicoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Listando agenda medica");

            var queryResult = _unitOfWork.MedicoRepository.SelectMany();

            if (queryResult == null)
                throw new NullReferenceException("Agenda medica nao encontrada!");

            var mappedResult = _mapper.Map<IEnumerable<MedicoViewModel>>(queryResult);

            return mappedResult;
        }
    }
}
