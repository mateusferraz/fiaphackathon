
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

namespace UnitTests.Application.Services.Pacientes
{
    public class CadastrarPacienteRequestHandlerTest
    {        
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CadatrarPacienteRequest _request;
        private readonly CadastrarPacienteRequestHandler _handler;
        private readonly ILogger<CadastrarPacienteRequestHandler> _mockLogger;

        public CadastrarPacienteRequestHandlerTest()
        {   
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _request = new CadatrarPacienteRequest();
            _handler = new CadastrarPacienteRequestHandler(                
                Substitute.For<ILogger<CadastrarPacienteRequestHandler>>(), _mockUnitOfWork.Object);
            _mockLogger = Substitute.For<ILogger<CadastrarPacienteRequestHandler>>();
        }

        [Fact]
        public void Should_insert_paciente_requested() 
        {
            var request = new CadatrarPacienteRequest
            {
                Documento="0",
                Email="Teste@teste.com.br",
                Nome="Teste da Silva",
                Senha="123"
            };

            Paciente paciente = null;
            _mockUnitOfWork.Setup(s => s.PacienteRepository
                    .SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>()))
                    .Returns(paciente);

            var handler = new CadastrarPacienteRequestHandler(_mockLogger, _mockUnitOfWork.Object);
            var response = handler.Handle(request, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_return_NullReferenceException_when_paciente_not_found()
        {
            _mockUnitOfWork.Setup(s => s.PacienteRepository
                .SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>()))
                .Throws(new NullReferenceException());
             await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_request, CancellationToken.None));
        }

        [Fact]
        public void Should_return_registered_client_invalid_operation_error()
        {
            var request = new CadatrarPacienteRequest
            {
                Documento="0",
                Email="Teste@teste.com.br",
                Nome="Teste da Silva",
                Senha="123"
            };

            Paciente paciente = new Paciente { Documento = "0"};
            _mockUnitOfWork.Setup(s => s.PacienteRepository
                    .SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>()))
                    .Returns(paciente);

            var handler = new CadastrarPacienteRequestHandler(_mockLogger, _mockUnitOfWork.Object);
            var exception = Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(request, CancellationToken.None)).Result;

            Assert.Equal("Paciente já cadastrado!", exception.Message);
        }
    }
}
