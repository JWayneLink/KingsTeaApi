using KTA.Data.Entity;
using KTA.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface IAccountService : IService
    {
        Task<ServiceResultModel<string>> AddAsync(AccountDto dtoItem);
        Task<ServiceResultModel<string>> DeleteAsync(AccountDto dtoItem);
        Task<ServiceResultModel<string>> UpdateAsync(AccountDto dtoItem);
        Task<ServiceResultModel<AccountEntity>> GetSingleItemAsync(string account);
        Task<ServiceResultModel<AccountEntity>> GetAllItemsAsync();
    }
}
