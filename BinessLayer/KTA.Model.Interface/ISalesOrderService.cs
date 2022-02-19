using KTA.Data.Entity;
using KTA.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface ISalesOrderService : IService
    {
        Task<ServiceResultModel<string>> AddAsync(SalesOrderDto dtoItem);
        Task<ServiceResultModel<string>> DeleteAsync(SalesOrderDto dtoItem);
        Task<ServiceResultModel<string>> UpdateAsync(SalesOrderDto dtoItem);
        Task<ServiceResultModel<SalesOrderEntity>> GetSingleItemAsync(string so);
        Task<ServiceResultModel<SalesOrderEntity>> GetAllItemsAsync();
    }
}
