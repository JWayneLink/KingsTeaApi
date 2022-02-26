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
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;
        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpPost, Route("AddSalesOrderAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiResultModel<string>> AddSalesOrderAsync(SalesOrderDto addSalesOrderDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._salesOrderService.AddAsync(addSalesOrderDto);
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

        [HttpPut, Route("UpdateSalesOrderAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiResultModel<string>> UpdateSalesOrderAsync(SalesOrderDto updateSalesOrderDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._salesOrderService.UpdateAsync(updateSalesOrderDto);
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

        [HttpDelete, Route("DeleteSalesOrderAsync")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ApiResultModel<string>> DeleteSalesOrderAsync(SalesOrderDto deleteSalesOrderDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._salesOrderService.DeleteAsync(deleteSalesOrderDto);
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

        [HttpGet, Route("GetSingleSalesOrderAsync")]
        public async Task<ApiResultModel<SalesOrderEntity>> GetSingleSalesOrderAsync(string so)
        {
            ApiResultModel<SalesOrderEntity> result = new ApiResultModel<SalesOrderEntity>();
            try
            {
                ServiceResultModel<SalesOrderEntity> serviceResult = await this._salesOrderService.GetSingleItemAsync(so);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<SalesOrderEntity>() { serviceResult.Data.FirstOrDefault() };
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpGet, Route("GetAllSalesOrdersAsync")]
        public async Task<ApiResultModel<SalesOrderEntity>> GetAllSalesOrdersAsync()
        {
            ApiResultModel<SalesOrderEntity> result = new ApiResultModel<SalesOrderEntity>();
            try
            {
                ServiceResultModel<SalesOrderEntity> serviceResult = await this._salesOrderService.GetAllItemsAsync();
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
    }
}
