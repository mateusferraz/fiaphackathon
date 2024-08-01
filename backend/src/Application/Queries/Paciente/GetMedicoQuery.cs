using Application.ViewMoldels;
using MediatR;

namespace Application.Queries.Paciente
{
    public class GetMedicoQuery : IRequest<MedicoViewModel>
    {
        public string Crm { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
    }
}
