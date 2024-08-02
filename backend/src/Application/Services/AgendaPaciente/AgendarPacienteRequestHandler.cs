using Application.Interfaces;
using Application.Requests.AgendaPaciente;
using Domain.Entidades;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Application.Services.AgendaPaciente
{
    public class AgendarPacienteRequestHandler : IRequestHandler<AgendarPacienteRequest, Unit>
    {
        private readonly ILogger<AgendarPacienteRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public AgendarPacienteRequestHandler(ILogger<AgendarPacienteRequestHandler> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task<Unit> Handle(AgendarPacienteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Realizando agendamento do paciente: {request.Documento}");

            var paciente = _unitOfWork.PacienteRepository.SelectOne(x => x.Documento == request.Documento)
                ?? throw new InvalidOperationException("Paciente não encontrado!");

            var agenda = _unitOfWork.AgendaRepository.SelectOne(x => x.Id == request.IdAgenda && x.Status == StatusAgendamento.Livre)
                ?? throw new InvalidOperationException("Agenda indisponivel!");

            _unitOfWork.AgendamentoRepository.Insert(new Agendamento
            {
                AgendaId = agenda.Id,
                PacienteId = paciente.Id
            });

            try
            {
                EnviarEmail(paciente, agenda);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar email: {ex.Message}");

                throw new InvalidOperationException("Erro ao enviar email para o medico: {agenda.Medico.Email}");
            }

            return Unit.Task;
        }

        public static void EnviarEmail(Paciente paciente, Agenda agenda)
        {
            MailMessage emailMessage = new MailMessage();

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);

            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 10000;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("fiaphackathonagendamedica@outlook.com", "paciente@123");
            emailMessage.From = new MailAddress("fiaphackathonagendamedica@outlook.com", "Fiap Hackathon");
            emailMessage.Subject = "”Health&Med - Nova consulta agendada";

            string mensagem = $@"Olá, {agenda.Medico.Nome}!
                    Você tem uma nova consulta marcada!
                    Paciente: {paciente.Nome}.
                    Data e horário: {agenda.DataAgendamento:dd/MM/yyyy} às {agenda.DataAgendamento:HH:mm}.";

            emailMessage.Body = mensagem;
            emailMessage.IsBodyHtml = true;
            emailMessage.Priority = MailPriority.Normal;
            emailMessage.To.Add(agenda.Medico.Email);

            smtpClient.Send(emailMessage);
        }

    }
}
