using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null);
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
