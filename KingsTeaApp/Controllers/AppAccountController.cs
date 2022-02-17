using KTA.Data.Entity;
using KTA.Model.Entities;
using KTA.Model.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ApiResultModel<string>> AddAccountAsync(AccountDto addAccountDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    string msg = await this._accountService.AddAsync(addAccountDto);
                    result.IsSuccess = true;
                    result.Message = msg;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Not OK";
                    return result;
                }
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
