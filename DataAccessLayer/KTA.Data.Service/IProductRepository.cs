using KTA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Service
{
    public interface IProductRepository : IRepositoryBase<ProductEntity>
    {
        Task<ProductEntity> GetSingleItemAsync(string pn);
    }
}
