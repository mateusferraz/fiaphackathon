using Application.Interfaces;
using Application.Requests.AgendaPaciente;
using Application.Services.AgendaPaciente;
using Domain.Entidades;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.Application.Services.Medicos
{
    public class AgendarPacienteRequestHandlerTests
    {
        private readonly Mock<ILogger<AgendarPacienteRequestHandler>> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly AgendarPacienteRequestHandler _handler;
        private readonly Mock<IEmailService> _emailService;

        public AgendarPacienteRequestHandlerTests()
        {
            _mockLogger = new Mock<ILogger<AgendarPacienteRequestHandler>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _emailService = new Mock<IEmailService>();
            _handler = new AgendarPacienteRequestHandler(_mockLogger.Object, _mockUnitOfWork.Object, _emailService.Object);
        }

        [Fact]
        public async Task Nao_deve_realizar_agendamento_se_paciente_nao_encontrado()
        {
            var request = new AgendarPacienteRequest { Documento = "123", IdAgenda = Guid.NewGuid() };

            _mockUnitOfWork.Setup(x => x.PacienteRepository.SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>()))
                .Returns((Paciente)null);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
            
            Assert.Equal("Paciente não encontrado!", exception.Message);
        }

        [Fact]
        public async Task Nao_deve_realizar_agendamento_se_agenda_escolhida_ja_agendada()
        {
            var paciente = new Paciente { Id = Guid.Parse("b96752a0-251f-497f-ab5f-8382caa8dc34"), Nome = "Paciente Teste" };

            var request = new AgendarPacienteRequest { Documento = "123", IdAgenda = Guid.NewGuid() };

            _mockUnitOfWork.Setup(x => x.PacienteRepository.SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>()))
                .Returns(paciente);

            _mockUnitOfWork.Setup(x => x.AgendaRepository.SelectOne(It.IsAny<Expression<Func<Agenda, bool>>>()))
               .Returns((Agenda)null);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));

            Assert.Equal("Agenda indisponivel!", exception.Message);
        }

        [Fact]
        public async Task Deve_realizar_agendamento_e_enviar_email_confirmando()
        {
            
            var request = new AgendarPacienteRequest { Documento = "123", IdAgenda = Guid.Parse("d9fc89b1-58f0-471a-b5d1-1c745e273fbf") };
            var paciente = new Paciente { Id = Guid.Parse("b96752a0-251f-497f-ab5f-8382caa8dc34"), Nome = "Paciente Teste" };
            var medico = new Medico { Id = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), Nome = "Dr. Joao" };
            var dataAgenda = DateTime.Now;
            var agenda = new Agenda { Id = Guid.Parse("d9fc89b1-58f0-471a-b5d1-1c745e273fbf"), Status = StatusAgendamento.Livre, MedicoId = Guid.Parse("227d288f-a248-4850-a1fc-c8539007fcbb"), DataAgendamento = dataAgenda };
            var agendamento = new Agendamento { AgendaId = agenda.Id, PacienteId = paciente.Id };

            _mockUnitOfWork.Setup(x => x.PacienteRepository.SelectOne(It.IsAny<Expression<Func<Paciente, bool>>>())).Returns(paciente);
            _mockUnitOfWork.Setup(x => x.AgendaRepository.SelectOne(It.IsAny<Expression<Func<Agenda, bool>>>())).Returns(agenda);
            _mockUnitOfWork.Setup(x => x.AgendamentoRepository.Insert(agendamento));
            _mockUnitOfWork.Setup(x => x.AgendaRepository.Update(agenda));

            await _handler.Handle(request, CancellationToken.None);

            _mockUnitOfWork.Verify(x => x.AgendamentoRepository.Insert(It.Is<Agendamento>(a => 
                a.PacienteId == paciente.Id &&
                a.AgendaId == agenda.Id)));

            _mockUnitOfWork.Verify(x => x.AgendaRepository.Update(It.Is<Agenda>(a => a.Id == agenda.Id && a.Status == StatusAgendamento.Agendado)), Times.Once);

            _emailService.Verify(service => service.EnviarEmail(paciente, agenda), Times.Once);
        }
    }
}
