using Application.Interfaces;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class MedicoRepository : BaseRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(DataBaseContext context) : base(context) { }

        public override Medico SelectOne(Expression<Func<Medico, bool>> filter = null)
        {
            IQueryable<Medico> query = context.Medicos;

            query = query
                .Include(account => account.Agendas);

            return query.Where(filter).FirstOrDefault();
        }

        public IEnumerable<Medico> SelectMany(Expression<Func<Medico, bool>> filter = null)
        {
            IQueryable<Medico> query = context.Medicos;

            query = query.Include(account => account.Agendas);

            return query.Where(filter).ToList();
        }
    }
}
