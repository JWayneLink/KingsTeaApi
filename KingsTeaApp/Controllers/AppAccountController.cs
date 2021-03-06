using KingsTeaApp.Filter;
using KTA.Data.Entity;
using KTA.Model.Constants;
using KTA.Model.Entities;
using KTA.Model.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace KingsTeaApp.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AppAccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AppAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost, Route("AddAccountAsync")]
        //[ValidateModel]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiResultModel<string>> AddAccountAsync(AccountDto addAccountDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._accountService.AddAsync(addAccountDto);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpPut, Route("UpdateAccountAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiResultModel<string>> UpdateAccountAsync(AccountDto updateAccountDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._accountService.UpdateAsync(updateAccountDto);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = true;
                result.Message = serviceResult.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpDelete, Route("DeleteAccountAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiResultModel<string>> DeleteAccountAsync(string accountId)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._accountService.DeleteAsync(accountId);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = true;
                result.Message = serviceResult.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpGet, Route("GetSingleAccountAsync")]
        public async Task<ApiResultModel<AccountEntity>> GetSingleAccountAsync(string account)
        {
            ApiResultModel<AccountEntity> result = new ApiResultModel<AccountEntity>();
            try
            {
                ServiceResultModel<AccountEntity> serviceResult = await this._accountService.GetSingleItemAsync(account);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<AccountEntity>() { serviceResult.Data.FirstOrDefault() };
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpGet, Route("GetAllAccountsAsync")]
        public async Task<ApiResultModel<AccountEntity>> GetAllAccountsAsync()
        {
            ApiResultModel<AccountEntity> result = new ApiResultModel<AccountEntity>();
            try
            {
                ServiceResultModel<AccountEntity> serviceResult = await this._accountService.GetAllItemsAsync();
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = serviceResult.Data;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpPost, Route("AccountLoginAsync")]
        public async Task<ApiResultModel<string>> AccountLoginAsync(AccountDto loginDto)
        {

            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._accountService.AuthValidation(loginDto);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<string>();
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpPost, Route("ForgotPasswordAsync")]
        public async Task<ApiResultModel<string>> ForgotPasswordAsync(AccountDto forgotPasswordDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._accountService.ForgotPassword(forgotPasswordDto.Account, forgotPasswordDto.Email);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<string>();
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }
    }
}
