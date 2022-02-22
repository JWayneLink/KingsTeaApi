using KTA.Data.Entity;
using KTA.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface IProductService : IService
    {

        Task<ServiceResultModel<string>> AddAsync(ProductDto dtoItem);
        Task<ServiceResultModel<string>> UpdateAsync(ProductDto dtoItem);
        Task<ServiceResultModel<string>> DeleteAsync(string pn);
        Task<ServiceResultModel<ProductEntity>> GetSingleItemAsync(string account);
        Task<ServiceResultModel<ProductEntity>> GetAllItemsAsync();
    }
}
