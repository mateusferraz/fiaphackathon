using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(DataBaseContext context) : base(context) { }

        public override Paciente SelectOne(Expression<Func<Paciente, bool>> filter = null)
        {
            IQueryable<Paciente> query = context.Pacientes;

            query = query
                .Include(account => account.Agendamentos);

            return query.Where(filter).FirstOrDefault();
        }
    }
}
