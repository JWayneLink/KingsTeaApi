using KTA.Data.Entity;
using KTA.Data.Service;
using KTA.Model.Entities;
using KTA.Model.Interface;
using KTA.Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository, IDateTimeService dateTimeService)
        {
            _accountRepository = accountRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<ServiceResultModel<string>> AddAsync(AccountDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var addItem = this.ConvertAccountEntity(dtoItem);
                AccountEntity existItem = await this._accountRepository.GetSingleItemAsync(addItem);
                if (existItem != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = AccountConstant.AccountExisted;
                    return serviceResult;
                }
                
                await this._accountRepository.AddAsync(addItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = AccountConstant.AccountInsertOK;
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess= false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }        

        public async Task<ServiceResultModel<string>> DeleteAsync(AccountDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var deleteItem = this.ConvertAccountEntity(dtoItem);
                AccountEntity existItem = await this._accountRepository.GetSingleItemAsync(deleteItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{deleteItem.Account} {AccountConstant.AccountDeleteDataNotFound}";
                    return serviceResult;
                }
                
                await this._accountRepository.DeleteAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{deleteItem.Account} {AccountConstant.AccountDeleteOK}";
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;               
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> UpdateAsync(AccountDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var updateItem = this.ConvertAccountEntity(dtoItem);
                AccountEntity existItem = await this._accountRepository.GetSingleItemAsync(updateItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{dtoItem.Account} {AccountConstant.AccountUpdateDataNotFound}";
                    return serviceResult;                    
                }

                existItem.Name = dtoItem.Name;
                existItem.Pwd = dtoItem.Pwd;
                existItem.Email = dtoItem.Email;
                existItem.Phone = dtoItem.Phone;
                existItem.Udt = this._dateTimeService.GetCurrentTime();
                await this._accountRepository.UpdateAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{updateItem.Account} {AccountConstant.AccountUpdateOK}";
                return serviceResult;                
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;                
            }
        }

        public async Task<ServiceResultModel<AccountEntity>> GetSingleItemAsync(string account)
        {
            ServiceResultModel<AccountEntity> serviceResult = new ServiceResultModel<AccountEntity>();
            try
            {                
                AccountEntity existItem = await this._accountRepository.GetSingleItemAsync(account);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{account} {AccountConstant.AccountQueryDataNotFound}";
                    serviceResult.Data = new List<AccountEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = AccountConstant.AccountQueryOK;
                serviceResult.Data = new List<AccountEntity>() { existItem };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<AccountEntity>();
                return serviceResult;
            }
        }

        private AccountEntity ConvertAccountEntity(AccountDto dtoItem)
        {
            AccountEntity dbItem = new AccountEntity();
            dbItem.Account = dtoItem.Account;
            dbItem.Name = dtoItem.Name;
            dbItem.Pwd = dtoItem.Pwd;
            dbItem.Email = dtoItem.Email;
            dbItem.Phone = dtoItem.Phone;
            dbItem.Cdt = _dateTimeService.GetCurrentTime();
            dbItem.Udt = _dateTimeService.GetCurrentTime();
            return dbItem;
        }

    }
}
