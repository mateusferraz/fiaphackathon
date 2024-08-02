using Application.Interfaces;
using Moq;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Application.Services.Accounts;
using Application.Requests.Medicos;
using Domain.Entidades;

namespace UnitTests.Application.Services.Medicos
{
    public class CadatrarAgendaMedicoRequestHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CadatrarAgendaMedicoRequest _request;
        private readonly CadatrarAgendaMedicoRequestHandler _handler;
        private readonly ILogger<CadatrarAgendaMedicoRequestHandler> _mockLogger;
        public CadatrarAgendaMedicoRequestHandlerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _request = new CadatrarAgendaMedicoRequest();
            _handler = new CadatrarAgendaMedicoRequestHandler(
                Substitute.For<ILogger<CadatrarAgendaMedicoRequestHandler>>(), _mockUnitOfWork.Object);
            _mockLogger = Substitute.For<ILogger<CadatrarAgendaMedicoRequestHandler>>();

        }

        [Fact]
        public async Task Should_return_agendamento_requested()
        {
            var request = new CadatrarAgendaMedicoRequest
            {
                DataHoraDisponivel=string.Empty,
                MedicoDocumento=string.Empty
            };

            Agendamento agendamento = null;
            _mockUnitOfWork.Setup(s => s.AgendamentoRepository
                    .SelectOne(It.IsAny<Expression<Func<Agendamento, bool>>>()))
                    .Returns(agendamento);

            var handler = new CadatrarAgendaMedicoRequestHandler(_mockLogger, _mockUnitOfWork.Object);
            await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task  Should_return_NullReferenceException_when_agendamento_not_found()
        {
            _mockUnitOfWork.Setup(s => s.AgendamentoRepository
                .SelectOne(It.IsAny<Expression<Func<Agendamento, bool>>>()))
                .Throws(new NullReferenceException());
            await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_request, CancellationToken.None));
        }
    }
}
