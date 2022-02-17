using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Service
{
    public interface IRepositoryBase<TEntity>
    {
        Task<int> AddAsync(TEntity item);
        Task<int> UpdateAsync(TEntity item);
        Task<TEntity> GetSingleItemAsync(TEntity item);
        Task<IEnumerable<TEntity>> GetAllItemsAsync();
        Task<int> DeleteAsync(TEntity item);
    }
}
