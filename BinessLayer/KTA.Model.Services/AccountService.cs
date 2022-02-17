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

        public async Task<string> AddAsync(AccountDto dtoItem)
        {
            try
            {
                var addItem = this.ConvertAccountEntity(dtoItem);
                AccountEntity existAcc = await this._accountRepository.GetSingleItemAsync(addItem);
                if (existAcc != null)
                {                    
                    return AccountConstant.AccountExisted;
                }

                await this._accountRepository.AddAsync(addItem);
                return AccountConstant.AccountInsertOK;
            }
            catch (Exception ex)
            {
                return ex.Message + ex.StackTrace;
            }
        }

        public Task<string> DeleteAsync(AccountDto dtoItem)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(AccountDto dtoItem)
        {
            throw new NotImplementedException();
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
