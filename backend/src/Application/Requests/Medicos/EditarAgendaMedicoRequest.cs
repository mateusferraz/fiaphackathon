using Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Requests.Medicos
{
    public class EditarAgendaMedicoRequest : IRequest<Unit>, IPersistable
    {
        public Guid idAgenda { get; set; }
        public string NovaDataHoraDisponivel { get ; set ; }

        [JsonIgnore]
        public string MedicoDocumento { get; set; }
    }
}
