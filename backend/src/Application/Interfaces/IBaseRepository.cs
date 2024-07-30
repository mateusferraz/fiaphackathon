using Domain.Entidades;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBaseRepository<T> where T : EntidadeBase
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T SelectToId(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate = null);
        T SelectOne(Expression<Func<T, bool>> filter = null);
        TColumn SelectOneColumn<TColumn>(Expression<Func<T, bool>> filter, Expression<Func<T, TColumn>> columnSelected);
    }
}
