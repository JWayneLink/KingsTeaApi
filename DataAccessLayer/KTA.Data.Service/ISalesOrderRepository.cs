using KTA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Data.Service
{
    public interface ISalesOrderRepository : IRepositoryBase<SalesOrderEntity>
    {
        Task<IEnumerable<SalesOrderEntity>> GetSingleItemAsync(string so);
        Task<int> AddBulkAsync(List<SalesOrderEntity> items);
        Task<List<string>> GetSalesOrderListAsync(string so);
    }
}
