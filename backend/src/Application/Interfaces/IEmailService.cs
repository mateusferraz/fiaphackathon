using Domain.Entidades;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        void EnviarEmail(Paciente paciente, Agenda agenda);
    }
}
