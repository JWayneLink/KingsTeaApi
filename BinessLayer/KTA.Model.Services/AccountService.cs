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
using KTA.Model.Library;

namespace KTA.Model.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IAccountRepository _accountRepository;
        private IEncryptService _encryptService;

        public AccountService(IAccountRepository accountRepository, IDateTimeService dateTimeService)
        {
            _accountRepository = accountRepository;
            _dateTimeService = dateTimeService;
            _encryptService = new MD5Wrapper();
        }

        public async Task<ServiceResultModel<string>> AddAsync(AccountDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var addItem = this.ConvertAccountEntity(dtoItem);

                string md5Hash = this._encryptService.ToMD5Hash(dtoItem.Pwd);
                string md16One = this._encryptService.Get16MD5One(dtoItem.Pwd);
                string md16Two = this._encryptService.Get16MD5Two(dtoItem.Pwd);
                string md32Two = this._encryptService.Get32MD5Two(dtoItem.Pwd);

                addItem.Pwd = this._encryptService.EncryptData(addItem.Account, addItem.Pwd); // MD5 encryption
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

        public async Task<ServiceResultModel<string>> DeleteAsync(string account)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {                
                AccountEntity existItem = await this._accountRepository.GetSingleItemAsync(account);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{account} {AccountConstant.AccountDeleteDataNotFound}";
                    return serviceResult;
                }
                
                await this._accountRepository.DeleteAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{account} {AccountConstant.AccountDeleteOK}";
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
                //existItem.Pwd = dtoItem.Pwd;
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

        public async Task<ServiceResultModel<AccountEntity>> GetAllItemsAsync()
        {
            ServiceResultModel<AccountEntity> serviceResult = new ServiceResultModel<AccountEntity>();            
            try
            {
                IEnumerable<AccountEntity> existItems = await this._accountRepository.GetAllItemsAsync();                
                if (existItems == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{AccountConstant.AccountQueryDataNotFound}";
                    serviceResult.Data = new List<AccountEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = AccountConstant.AccountQueryOK;
                serviceResult.Data = existItems.ToList();
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

        public async Task<ServiceResultModel<string>> AuthValidation(AccountDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                AccountEntity existItem = await this._accountRepository.GetSingleItemAsync(dtoItem.Account);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = false;
                    serviceResult.Message = $"{dtoItem.Account} {AccountConstant.AccountQueryDataNotFound}. Please Sign up new account.";
                    serviceResult.Data = new List<string>();
                    return serviceResult;
                }

                string encryptedPwd = _encryptService.EncryptData(dtoItem.Account, dtoItem.Pwd); // MD5 encryption
                if (!existItem.Pwd.Equals(encryptedPwd))
                {
                    serviceResult.IsSuccess = false;
                    serviceResult.Message = $"Account {dtoItem.Account} login fail. Password Incorrect.";
                    serviceResult.Data = new List<string>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = $"Account {dtoItem.Account} login success.";
                serviceResult.Data = new List<string>();
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<string>();
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> ForgotPassword(string account, string email)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                AccountEntity existItem = await this._accountRepository.GetAccountByEmail(account, email);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = false;
                    serviceResult.Message = $"{account} {AccountConstant.AccountQueryDataNotFound}. Please Sign up new account.";
                    serviceResult.Data = new List<string>();
                    return serviceResult;
                }

                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.From = new System.Net.Mail.MailAddress("kingsteakingstea@gmail.com");
                mailMsg.To.Add(email); //設定收件者Email
                //mailMsg.Bcc.Add("密件副本的收件者Mail"); //加入密件副本的Mail          
                mailMsg.Subject = "Kings Tea Shop Account New System Password";
                string sysPassword = Guid.NewGuid().ToString();
                mailMsg.Body = $"<h1>Password:  {sysPassword} </h1>"; //設定信件內容
                mailMsg.IsBodyHtml = true; //是否使用html格式
                System.Net.Mail.SmtpClient MySMTP = new System.Net.Mail.SmtpClient();
                MySMTP.Credentials = new System.Net.NetworkCredential("kingsteakingstea@gmail.com", "Kingstea1234");

                MySMTP.Host = "smtp.gmail.com"; //設定smtp Server
                MySMTP.Port = 25; //設定Port
                MySMTP.EnableSsl = true; //gmail預設開啟驗證
               
                try
                {
                    MySMTP.Send(mailMsg);
                    
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    //釋放資源
                    mailMsg.Dispose();
                    MySMTP.Dispose();
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = $"Please receive E-mail for getting new system password";
                serviceResult.Data = new List<string>();
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<string>();
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
