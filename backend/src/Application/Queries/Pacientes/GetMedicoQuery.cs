using Application.ViewMoldels;
using MediatR;

namespace Application.Queries.Pacientes
{
    public class GetMedicoQuery : IRequest<IEnumerable<MedicoViewModel>>
    {
        public string Documento { get; set; }
    }
}
