
using Application.Interfaces;
using Moq;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Application.Services.Accounts;
using Domain.Entidades;
using Application.Requests.Pacientes;
using Application.Services.Pacientes;
using Domain.Entities;
using AutoMapper;
using MediatR;
using Application.Queries.Pacientes;
using UnitTests.Application.Services.Pacientes;
using NSubstitute.ExceptionExtensions;
using System;

namespace UnitTests.Application.Services.Pacientes
{
    public class GetMedicoQueryHandlerTest
    {
        private readonly GetMedicoQueryHandler _handler;
        private readonly IUnitOfWork _mockUnitOfWork;
        private readonly ILogger<GetMedicoQueryHandler> _logger;

        public GetMedicoQueryHandlerTest()
        {
            _mockUnitOfWork = Substitute.For<IUnitOfWork>();
            _logger = Substitute.For<ILogger<GetMedicoQueryHandler>>();
            _handler = new GetMedicoQueryHandler(
                _mockUnitOfWork,
                _logger,
                Substitute.For<IMapper>()
                );
        }

        [Fact]
        public void Should_return_medico_requested()
        {
            var _query = new GetMedicoQuery { Documento = "0" };
            var medicosAgenda = FakeData.GetMedicos();

            _mockUnitOfWork.MedicoRepository.SelectMany().Returns(medicosAgenda);
            var response = _handler.Handle(_query, CancellationToken.None);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_return_NullReferenceException_when_medico_not_found()
        {
            var _query = new GetMedicoQuery { Documento = "0" };
          
            _mockUnitOfWork.MedicoRepository.SelectMany().Returns((IEnumerable<Medico>)null);
            var excpt = await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_query, CancellationToken.None));

            Assert.Equal("Agenda medica nao encontrada!", excpt.Message);
        }
    }
}
