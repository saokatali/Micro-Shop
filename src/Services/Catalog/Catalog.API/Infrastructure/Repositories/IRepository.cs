using Catalog.API.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Catalog.API.Infrastructure.Repositories
{
    interface IRepository
    {
        public interface IRepository<T> where T: BaseEntity
        {
            Task<T> GetByIdAsync(Guid id);
            Task<IList<T>> GetAllAsync();

            Task<IList<T>> GetPagedListAsync(int pageNo, int records);

            Task<IList<T>> GetByConditionAsync(Expression<Func<T, bool>> condition);

            Task<IList<T>> GetPagedListByConditionAsync(Expression<Func<T, bool>> condition, int pageNo, int records);

            Task<T> AddAsync(T entity);
            Task<int> UpdateAsync(T entity);

            Task<int> DeleteAsync(T entity);


        }
    }
}
