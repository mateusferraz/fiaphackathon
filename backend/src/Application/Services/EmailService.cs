using Application.Interfaces;
using Domain.Entidades;
using Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        public void EnviarEmail(Paciente paciente, Agenda agenda)
        {
            using MailMessage emailMessage = new();
            using SmtpClient smtpClient = new("smtp-mail.outlook.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 10000;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("fiaphackathonagendamedica@outlook.com", "paciente@123");
            emailMessage.From = new MailAddress("fiaphackathonagendamedica@outlook.com", "Fiap Hackathon");
            emailMessage.Subject = "Health&Med - Nova consulta agendada";

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
