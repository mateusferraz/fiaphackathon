using Application.Interfaces;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AgendamentoRepository : BaseRepository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(DataBaseContext context) : base(context) { }

        public override Agendamento SelectOne(Expression<Func<Agendamento, bool>> filter = null)
        {
            IQueryable<Agendamento> query = context.Agendamentos;

            query = query
                .Include(account => account.Paciente)
                .Include(account => account.AgendaMedico);

            return query.Where(filter).FirstOrDefault();
        }
    }
}
