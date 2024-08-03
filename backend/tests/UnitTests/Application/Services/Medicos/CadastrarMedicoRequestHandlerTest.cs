
using Application.Interfaces;
using Moq;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Application.Services.Accounts;
using Application.Requests.Medicos;
using Domain.Entidades;
using System.Reflection.Metadata;
using Azure.Core;

namespace UnitTests.Application.Services.Medicos
{
    public class CadastrarPacienteRequestHandlerTest
    {        
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CadatrarMedicoRequest _request;
        private readonly CadatrarMedicoRequestHandler _handler;
        private readonly ILogger<CadatrarMedicoRequestHandler> _mockLogger;

        public CadastrarPacienteRequestHandlerTest()
        {   
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _request = new CadatrarMedicoRequest();
            _handler = new CadatrarMedicoRequestHandler(                
                Substitute.For<ILogger<CadatrarMedicoRequestHandler>>(), _mockUnitOfWork.Object);
            _mockLogger = Substitute.For<ILogger<CadatrarMedicoRequestHandler>>();
        }

        [Fact]
        public void Should_insert_medico_requested() 
        {
            var request = new CadatrarMedicoRequest
            {
                Crm = "0",
                Documento="0",
                Email="Teste@teste.com.br",
                Nome="Teste da Silva",
                Senha="123"
            };

            Medico medico = null;
            _mockUnitOfWork.Setup(s => s.MedicoRepository
                    .SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                    .Returns(medico);

            var handler = new CadatrarMedicoRequestHandler(_mockLogger, _mockUnitOfWork.Object);
            var response = handler.Handle(request, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_return_NullReferenceException_when_medico_not_found()
        {
            _mockUnitOfWork.Setup(s => s.MedicoRepository
                .SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Throws(new NullReferenceException());
             await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_request, CancellationToken.None));
        }
    }
}
