
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
    public class CadastrarMedicoRequestHandlerTest
    {        
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CadastrarMedicoRequest _request;
        private readonly CadastrarMedicoRequestHandler _handler;
        private readonly ILogger<CadastrarMedicoRequestHandler> _mockLogger;

        public CadastrarMedicoRequestHandlerTest()
        {   
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _request = new CadastrarMedicoRequest();
            _handler = new CadastrarMedicoRequestHandler(                
                Substitute.For<ILogger<CadastrarMedicoRequestHandler>>(), _mockUnitOfWork.Object);
            _mockLogger = Substitute.For<ILogger<CadastrarMedicoRequestHandler>>();
        }

        [Fact]
        public async Task Should_not_return_medico_requested() 
        {
            var request = new CadastrarMedicoRequest
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

            var handler = new CadastrarMedicoRequestHandler(_mockLogger, _mockUnitOfWork.Object);
            await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(request, CancellationToken.None));            
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
