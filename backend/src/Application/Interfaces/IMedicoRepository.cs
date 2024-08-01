using Domain.Entidades;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IMedicoRepository : IBaseRepository<Medico>
    {
        IEnumerable<Medico> SelectMany(Expression<Func<Medico, bool>> filter = null);
    }
}
