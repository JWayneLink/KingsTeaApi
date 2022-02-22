using KingsTeaApp.Filter;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost, Route("AddCustomerAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> AddCustomerAsync(CustomerDto addCustomerDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._customerService.AddAsync(addCustomerDto);
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

        [HttpPut, Route("UpdateCustomerAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> UpdateCustomerAsync(CustomerDto updateCustomertDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._customerService.UpdateAsync(updateCustomertDto);
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

        [HttpDelete, Route("DeleteCustomerAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> DeleteCustomerAsync(CustomerDto deleteCustomerDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._customerService.DeleteAsync(deleteCustomerDto);
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

        [HttpGet, Route("GetSingleCustomerAsync")]
        public async Task<ApiResultModel<CustomerEntity>> GetSingleCustomerAsync(string custId)
        {
            ApiResultModel<CustomerEntity> result = new ApiResultModel<CustomerEntity>();
            try
            {
                ServiceResultModel<CustomerEntity> serviceResult = await this._customerService.GetSingleItemAsync(custId);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<CustomerEntity>() { serviceResult.Data.FirstOrDefault() };
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpGet, Route("GetAllCustomersAsync")]
        public async Task<ApiResultModel<CustomerEntity>> GetAllCustomersAsync()
        {
            ApiResultModel<CustomerEntity> result = new ApiResultModel<CustomerEntity>();
            try
            {
                ServiceResultModel<CustomerEntity> serviceResult = await this._customerService.GetAllItemsAsync();
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

        [HttpGet, Route("GetDummyCustomerAsync")]
        public async Task<ApiResultModel<DummyCustomerDto>> GetDummyCustomerAsync(string id)
        {
            // https://jsonplaceholder.typicode.com/users/1
            ApiResultModel<DummyCustomerDto> result = new ApiResultModel<DummyCustomerDto>();
            try
            {
                ServiceResultModel<DummyCustomerDto> serviceResult = await this._customerService.GetDummyCustomers(id);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<DummyCustomerDto>() { serviceResult.Data.FirstOrDefault() };
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
