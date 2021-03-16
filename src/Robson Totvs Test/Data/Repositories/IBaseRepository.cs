using Robson_Totvs_Test.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Data.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
        Task<T[]> FindAllAsync(Expression<Func<T, bool>> filter);
        Task<bool> AddAsync(T newObj);
    }
}
