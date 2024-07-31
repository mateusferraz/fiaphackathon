using Application.Interfaces;
using Domain.Entidades;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AgendaRepository: BaseRepository<Agenda>, IAgendaRepository
    {
        public AgendaRepository(DataBaseContext context) : base(context) { }

        public override Agenda SelectOne(Expression<Func<Agenda, bool>> filter = null)
        {
            IQueryable<Agenda> query = context.Agendas;

            query = query
                .Include(account => account.Medico)
                .Include(account => account.AgendamentoPaciente);

            return query.Where(filter).FirstOrDefault();
        }
    }
}
