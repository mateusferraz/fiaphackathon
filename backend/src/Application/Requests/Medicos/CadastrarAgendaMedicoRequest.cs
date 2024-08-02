using Application.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Requests.Medicos
{
    public class CadastrarAgendaMedicoRequest : IRequest<Unit>, IPersistable
    {
        public string DataHoraDisponivel { get; set; }

        [JsonIgnore]
        public string MedicoDocumento { get; set; }
    }
}
