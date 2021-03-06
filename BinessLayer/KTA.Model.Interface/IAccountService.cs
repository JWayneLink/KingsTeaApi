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
        Task<ServiceResultModel<string>> DeleteAsync(string account);
        Task<ServiceResultModel<string>> UpdateAsync(AccountDto dtoItem);
        Task<ServiceResultModel<AccountEntity>> GetSingleItemAsync(string account);
        Task<ServiceResultModel<AccountEntity>> GetAllItemsAsync();
        Task<ServiceResultModel<string>> AuthValidation(AccountDto dtoItem);
        Task<ServiceResultModel<string>> ForgotPassword(string account, string email);
    }
}
