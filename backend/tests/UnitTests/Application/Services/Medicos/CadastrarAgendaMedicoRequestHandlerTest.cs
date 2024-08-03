using Application.Interfaces;
using Application.Requests.Medicos;
using Application.Services.Accounts;
using Domain.Entidades;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System.Linq.Expressions;

namespace UnitTests.Application.Services.Medicos
{
    public class CadastrarAgendaMedicoRequestHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly CadastrarAgendaMedicoRequest _request;
        private readonly CadastrarAgendaMedicoRequestHandler _handler;
        private readonly ILogger<CadastrarAgendaMedicoRequestHandler> _mockLogger;
        public CadastrarAgendaMedicoRequestHandlerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _request = new CadastrarAgendaMedicoRequest();
            _handler = new CadastrarAgendaMedicoRequestHandler(
                Substitute.For<ILogger<CadastrarAgendaMedicoRequestHandler>>(), _mockUnitOfWork.Object);
            _mockLogger = Substitute.For<ILogger<CadastrarAgendaMedicoRequestHandler>>();

        }

        [Fact]
        public async Task Nao_deve_registrar_agenda_se_medico_nao_logado()
        {
            var request = new CadastrarAgendaMedicoRequest
            {
                DataHoraDisponivel = string.Empty,
                MedicoDocumento = string.Empty
            };

            _mockUnitOfWork.Setup(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns((Medico)null);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("Medico não está logado!", exception.Message);
        }

        [Fact]
        public async Task Não_deve_registrar_agenda_se_formato_data_invalido()
        {
            var medico = new Medico { Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao", Documento = "123456" };

            var request = new CadastrarAgendaMedicoRequest
            {
                DataHoraDisponivel = DateTime.Now.ToString(),
                MedicoDocumento = medico.Documento
            };

            _mockUnitOfWork.Setup(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns(medico);
            
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("Formato de data inválida, por favor informe a data e hora no formato: \"dia/mês/ano hora:minuto\" ex:01/01/2000 12:00", exception.Message);
        }

        [Fact]
        public async Task Não_deve_registrar_agenda_com_data_menor_que_data_atual()
        {
            var medico = new Medico { Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao", Documento = "123456" };

            var request = new CadastrarAgendaMedicoRequest
            {
                DataHoraDisponivel = "10/08/2023 12:00",
                MedicoDocumento = medico.Documento
            };

            _mockUnitOfWork.Setup(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns(medico);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("Data não pode ser menor que data atual!", exception.Message);
        }

        [Fact]
        public async Task Não_deve_registrar_agenda_com_data_ja_cadastrada()
        {
            var dataAgenda = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm");
            var medico = new Medico { Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao", Documento = "123456" };
            var agenda = new Agenda { Id = Guid.Parse("d9fc89b1-58f0-471a-b5d1-1c745e273fbf"), Status = StatusAgendamento.Livre, MedicoId = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), DataAgendamento = DateTime.Parse(dataAgenda) };

            var request = new CadastrarAgendaMedicoRequest
            {
                DataHoraDisponivel = dataAgenda,
                MedicoDocumento = medico.Documento
            };

            _mockUnitOfWork.Setup(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns(medico);

            _mockUnitOfWork.Setup(x => x.AgendaRepository.SelectOne(It.IsAny<Expression<Func<Agenda, bool>>>()))
                .Returns(agenda);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("Data já cadastrado!", exception.Message);
        }

        [Fact]
        public async Task Deve_registrar_agenda_corretamente()
        {
            var medico = new Medico { Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao", Documento = "123456" };
           
            var dataAgenda =DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm"); 
            var request = new CadastrarAgendaMedicoRequest
            {
                DataHoraDisponivel = dataAgenda,
                MedicoDocumento = medico.Documento
            };

            var agenda = new Agenda
            { 
                Id = Guid.Parse("d9fc89b1-58f0-471a-b5d1-1c745e273fbf"), 
                Status = StatusAgendamento.Livre, 
                MedicoId = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), 
                DataAgendamento = DateTime.Parse(request.DataHoraDisponivel) 
            };

            _mockUnitOfWork.Setup(x => x.MedicoRepository.SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Returns(medico);
            _mockUnitOfWork.Setup(x => x.AgendaRepository.Insert(agenda));
            
            await _handler.Handle(request, CancellationToken.None);

            _mockUnitOfWork.Verify(x => x.AgendaRepository.Insert(It.Is<Agenda>(
                a => a.Status == StatusAgendamento.Livre &&
                a.MedicoId == medico.Id && 
                a.DataAgendamento == agenda.DataAgendamento)), Times.Once);
        }
    }
}
