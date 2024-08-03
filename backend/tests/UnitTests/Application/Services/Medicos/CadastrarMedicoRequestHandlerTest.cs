
using Application.Interfaces;
using Application.Requests.Medicos;
using Application.Services.Accounts;
using Domain.Entidades;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System.Linq.Expressions;

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
        public async Task Nao_deve_duplicar_cadastro_medico()
        {
            var medico = new Medico { Documento = "12345678", Email = "Teste@teste.com.br", Crm = "0", Senha ="123", Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao" };

            var request = new CadatrarMedicoRequest
            {
                Crm = "0",
                Documento = "12345678",
                Email = "Teste@teste.com.br",
                Nome = "Dr. Joao",
                Senha = "123"
            };

            _mockUnitOfWork.Setup(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns(medico);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("Medico já cadastrado!", exception.Message);
        }

        [Fact]
        public async Task Nao_deve_cadastrar_medico_se_email_ja_cadastrado()
        {
            var medico = new Medico { Documento = "12345678", Email = "Teste@teste.com.br", Crm = "0", Senha = "123", Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao" };

            var request = new CadatrarMedicoRequest
            {
                Crm = "1",
                Documento = "123456789",
                Email = "Teste@teste.com.br",
                Nome = "Dr. Joao",
                Senha = "123"
            };

            _mockUnitOfWork.SetupSequence(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns((Medico)null)
                .Returns(medico);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("E-mail Medico já cadastrado!", exception.Message);
        }

        [Fact]
        public async Task Nao_deve_cadastrar_medico_se_crm_ja_cadastrado()
        {
            var medico = new Medico { Documento = "12345678", Email = "Teste@teste.com.br", Crm = "1", Senha = "123", Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao" };

            var request = new CadatrarMedicoRequest
            {
                Crm = "1",
                Documento = "1234567890",
                Email = "Tester@teste.com.br",
                Nome = "Dr. Joao",
                Senha = "123"
            };

            _mockUnitOfWork.SetupSequence(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns((Medico)null)
                .Returns((Medico)null)
                .Returns(medico);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("CRM Medico já cadastrado!", exception.Message);
        }
    }
}
