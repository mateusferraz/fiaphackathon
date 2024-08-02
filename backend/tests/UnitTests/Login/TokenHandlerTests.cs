using Application.Interfaces;
using Application.Requests;
using Application.Services;
using Domain.Entidades;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace UnitTests.Application.Requests
{
    public class TokenHandlerTests
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ILogger<TokenRequestHandler> _mockLogger;
        private readonly ITokenService _mockTokenService;


        public TokenHandlerTests()
        {

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockLogger = Substitute.For<ILogger<TokenRequestHandler>>();
            _mockTokenService = Substitute.For<ITokenService>();
        }

        [Fact]
        public async void Should_return_ArgumentException_when_medico_invalid()
        {
            var query = new TokenRequest { Email = "teste@teste.com.br",  Senha = "1234", TipoUsuario = TipoUsuario.Medico };

            _mockUnitOfWork.Setup(s => s.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>())).Returns((Medico)null);

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

    }
}
