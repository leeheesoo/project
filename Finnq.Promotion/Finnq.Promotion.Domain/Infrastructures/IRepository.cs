using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Finnq.Promotion.Domain.Infrastructures {
    public interface IRepository<TEntity> where TEntity : class {

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);

        void Remove(TEntity entity);

        TEntity Update(TEntity entity);

        void Save();

        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
