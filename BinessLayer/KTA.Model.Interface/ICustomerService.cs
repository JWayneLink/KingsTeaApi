using KTA.Data.Entity;
using KTA.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface ICustomerService : IService
    {
        Task<ServiceResultModel<string>> AddAsync(CustomerDto dtoItem);
        Task<ServiceResultModel<string>> DeleteAsync(CustomerDto dtoItem);
        Task<ServiceResultModel<string>> UpdateAsync(CustomerDto dtoItem);
        Task<ServiceResultModel<CustomerEntity>> GetSingleItemAsync(string custId);
        Task<ServiceResultModel<CustomerEntity>> GetAllItemsAsync();
    }
}
