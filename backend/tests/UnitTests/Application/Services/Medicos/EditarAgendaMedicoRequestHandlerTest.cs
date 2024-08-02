
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
using Infrastructure.Repositories;

namespace UnitTests.Application.Services.Medicos
{
    public class EditarAgendaMedicoRequestHandlerTest
    {        
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly EditarAgendaMedicoRequest _request;
        private readonly EditarAgendaMedicoRequestHandler _handler;
        private readonly ILogger<EditarAgendaMedicoRequestHandler> _mockLogger;

        public EditarAgendaMedicoRequestHandlerTest()
        {   
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _request = new EditarAgendaMedicoRequest();
            _handler = new EditarAgendaMedicoRequestHandler(                
                Substitute.For<ILogger<EditarAgendaMedicoRequestHandler>>(), _mockUnitOfWork.Object);
            _mockLogger = Substitute.For<ILogger<EditarAgendaMedicoRequestHandler>>();
        }

        #region sucess
        [Fact]
        public void Should_edit_medico_requested() 
        {
            var idAgenda = Guid.NewGuid();
            var request = new EditarAgendaMedicoRequest
            {
                idAgenda = idAgenda,
                MedicoDocumento ="1234",
                NovaDataHoraDisponivel="25/10/2024 15:00"
            };
            
            Medico medico = new Medico
            {
                Nome = "mockMedico",
                Documento = "1234",
                Crm = "12",
                Email = "mock@mock",
                Id = Guid.NewGuid()                
            };
            Agenda agenda = new Agenda
            {
                Id = idAgenda,
                DataAgendamento = DateTime.Now,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Medico = medico,
                MedicoId = medico.Id

            };
            medico.Agendas = new List<Agenda> { agenda };
            _mockUnitOfWork.SetupSequence(s => s.MedicoRepository
                    .SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                    .Returns(medico);
          
            _mockUnitOfWork.SetupSequence(s => s.AgendaRepository
                    .SelectOne(It.IsAny<Expression<Func<Agenda, bool>>>()))
                    .Returns(value:null)
                    .Returns(agenda);

            _mockUnitOfWork.Setup(s => s.AgendamentoRepository
                   .SelectOne(It.IsAny<Expression<Func<Agendamento, bool>>>()))
                   .Returns(value: null);

            _mockUnitOfWork.Setup(s => s.AgendaRepository
                 .Delete(It.IsAny<Agenda>()));
            _mockUnitOfWork.Setup(s => s.AgendaRepository
                .Insert(It.IsAny<Agenda>()));
            var handler = new EditarAgendaMedicoRequestHandler(_mockLogger, _mockUnitOfWork.Object);
            var response = handler.Handle(request, CancellationToken.None);

            Assert.NotNull(response);
        }
        #endregion

        #region fails
        [Fact]
        public async Task Should_return_NullReferenceException_when_medico_not_found()
        {
            _mockUnitOfWork.Setup(s => s.MedicoRepository
                .SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
                .Throws(new NullReferenceException());
             await Assert.ThrowsAsync<NullReferenceException>(() => _handler.Handle(_request, CancellationToken.None));
        }

        //[Fact]
        //public async Task Should_Fails_WhenDate_IsInvalidFormat()
        //{
        //    //arrange
        //    var idAgenda = Guid.NewGuid();
        //    var request = new EditarAgendaMedicoRequest
        //    {
        //        idAgenda = idAgenda,
        //        MedicoDocumento = "1234",
        //        NovaDataHoraDisponivel = "25/10/2024 15:00:xx"
        //    };

        //    Medico medico = new Medico
        //    {
        //        Nome = "mockMedico",
        //        Documento = "1234",
        //        Crm = "12",
        //        Email = "mock@mock",
        //        Id = Guid.NewGuid()
        //    };
        //    _mockUnitOfWork.SetupSequence(s => s.MedicoRepository
        //            .SelectOne(It.IsAny<Expression<Func<Medico, bool>>>()))
        //            .Returns(medico);
        //    var handler = new EditarAgendaMedicoRequestHandler(_mockLogger, _mockUnitOfWork.Object);
        //    var response = handler.Handle(request, CancellationToken.None);

        //    Action act = () => _handler.Handle(_request, CancellationToken.None);
        //    //assert
        //    InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
        //    //The thrown exception can be used for even more detailed assertions.
        //    Assert.Contains("Formato de data inválida", exception.Message);
        //}
        //#endregion
    }
}
