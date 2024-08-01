
using AutoMapper;
using Application.Interfaces;
using Moq;
using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Linq.Expressions;
using Application.Services.Accounts;
using Application.Requests.Medicos;
using Domain.Entidades;

namespace UnitTests.Application.Services.Medicos
{
    public class CadastrarMedicoRequestHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CadastrarMedicoRequest _request;
        private readonly CadastrarMedicoRequestHandler _handler;

        public CadastrarMedicoRequestHandlerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(s => s.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>())).Returns(new Medico());
            _request = new CadastrarMedicoRequest();
            _handler = new CadastrarMedicoRequestHandler(                
                Substitute.For<ILogger<CadastrarMedicoRequestHandler>>(),
                _mockUnitOfWork.Object
                );
        }

        [Fact]        
        public void Should_return_client_requested()
        {
            var response = _handler.Handle(_request, CancellationToken.None);

            Assert.NotNull(response);
        }

        [Fact]
        public void Should_return_NullReferenceException_when_client_not_found()
        {
            _mockUnitOfWork.Setup(s => s.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>())).Throws(new NullReferenceException());
            Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_request, CancellationToken.None));
        }

    }
}
