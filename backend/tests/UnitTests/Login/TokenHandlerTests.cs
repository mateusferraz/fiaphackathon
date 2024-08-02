using Application.Interfaces;
using Application.Requests;
using Application.Services;
using Domain.Entidades;
using Domain.Entities;
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

        [Fact]
        public async void Should_return_token_when_medico_valid()
        {
            var query = new TokenRequest { Email = "medico@teste.com.br", Senha = "1234", TipoUsuario = TipoUsuario.Medico };
            var medico = new Medico { Documento = "123456789" };

            _mockUnitOfWork.Setup(s => s.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>())).
                Returns(medico);
            _mockTokenService.GerarToken(Arg.Any<string>(), Arg.Any<TipoUsuario>()).Returns("token");

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal("token", result);
        }

        [Fact]
        public async void Should_return_token_when_paciente_valid()
        {
            var query = new TokenRequest { Email = "paciente@teste.com.br", Senha = "1234", TipoUsuario = TipoUsuario.Paciente };
            var paciente = new Paciente { Documento = "987654321" };

            _mockUnitOfWork.Setup(s => s.PacienteRepository.SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>())).
                Returns(paciente);
            _mockTokenService.GerarToken(Arg.Any<string>(), Arg.Any<TipoUsuario>()).Returns("token");

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal("token", result);
        }

        [Fact]
        public async void Should_return_ArgumentException_when_paciente_invalid()
        {
            var query = new TokenRequest { Email = "teste@teste.com.br", Senha = "1234", TipoUsuario = TipoUsuario.Paciente };

            _mockUnitOfWork.Setup(s => s.PacienteRepository.SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>())).
                Returns((Paciente)null);

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async void Should_call_GerarToken_with_correct_parameters()
        {
            var query = new TokenRequest { Email = "medico@teste.com.br", Senha = "1234", TipoUsuario = TipoUsuario.Medico };
            var medico = new Medico { Documento = "123456789" };

            _mockUnitOfWork.Setup(s => s.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>())).
                Returns(medico);
            _mockTokenService.GerarToken(Arg.Any<string>(), Arg.Any<TipoUsuario>()).Returns("token");

            var handler = new TokenRequestHandler(_mockUnitOfWork.Object, _mockLogger, _mockTokenService);

            await handler.Handle(query, CancellationToken.None);

            _mockTokenService.Received().GerarToken("123456789", TipoUsuario.Medico);
        }
    }
}

