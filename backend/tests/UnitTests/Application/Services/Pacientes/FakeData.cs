using Application.Queries.Pacientes;
using Azure.Core;
using Domain.Entidades;
using System.Globalization;

namespace UnitTests.Application.Services.Pacientes
{
    public class FakeData
    {
        public static List<Medico> GetMedicos()
        {
            var listMedico = new List<Medico>();

            var medico = new Medico
            {
                Id = new Guid(),
                Documento = "1111",
                Crm = "222",
                DataCriacao = DateTime.Now,
                Email = "medico222@teste.com.br",
                Nome = "Medico teste 2"
            };
            var listaAgenda = new List<Agenda>();
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(30*1).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(30*2).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(30*3).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(30*4).ToString("dd/MM/yyyy HH:mm")));
            medico.Agendas = listaAgenda;
            listMedico.Add(medico);

            var medico2 = new Medico
            {
                Id = new Guid(),
                Documento = "3333",
                Crm = "488",
                DataCriacao = DateTime.Now,
                Email = "medico488@teste.com.br",
                Nome = "Medico teste 488"
            };
            listaAgenda = new List<Agenda>();
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(15*1).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(15*2).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(15*3).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(15*4).ToString("dd/MM/yyyy HH:mm")));
            listaAgenda.Add(GetAgenda(medico, DateTime.Now.AddDays(1).AddMinutes(15*5).ToString("dd/MM/yyyy HH:mm")));
            medico2.Agendas = listaAgenda;
            listMedico.Add(medico2);


            return listMedico;
        }

        public static Agenda GetAgenda(Medico medico, string dataAgendamento)
        {
            string format = "dd/MM/yyyy HH:mm";
            DateTime.TryParseExact(dataAgendamento, 
                                            format, 
                      CultureInfo.InvariantCulture, 
                               DateTimeStyles.None, 
                            out DateTime resultDate);

            var agenda = new Agenda
            {
                DataCriacao = DateTime.Now,
                Status = Domain.Enums.StatusAgendamento.Livre,
                DataAgendamento = resultDate,
                Medico = medico,
                MedicoId = medico.Id
            };

            return agenda;
        }
    }
}