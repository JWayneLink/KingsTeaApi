using KTA.Data.Entity;
using KTA.Data.Service;
using KTA.Model.Constants;
using KTA.Model.Entities;
using KTA.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ISalesOrderRepository _salesOrderRepository;

        public SalesOrderService(ISalesOrderRepository salesOrderRepository, IDateTimeService dateTimeService)
        {
            _salesOrderRepository = salesOrderRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<ServiceResultModel<string>> AddAsync(SalesOrderDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var addItem = this.ConvertSalesOrderEntity(dtoItem);
                SalesOrderEntity existItem = await this._salesOrderRepository.GetSingleItemAsync(addItem);
                if (existItem != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = SalesOrderConstant.SalesOrderExisted;
                    return serviceResult;
                }

                await this._salesOrderRepository.AddAsync(addItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderInsertOK;
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> DeleteAsync(SalesOrderDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var deleteItem = this.ConvertSalesOrderEntity(dtoItem);
                SalesOrderEntity existItem = await this._salesOrderRepository.GetSingleItemAsync(deleteItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{deleteItem.SO} {SalesOrderConstant.SalesOrderDeleteDataNotFound}";
                    return serviceResult;
                }

                await this._salesOrderRepository.DeleteAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{deleteItem.SO} {SalesOrderConstant.SalesOrderDeleteOK}";
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> UpdateAsync(SalesOrderDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var updateItem = this.ConvertSalesOrderEntity(dtoItem);
                SalesOrderEntity existItem = await this._salesOrderRepository.GetSingleItemAsync(updateItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{dtoItem.SO} {SalesOrderConstant.SalesOrderUpdateDataNotFound}";
                    return serviceResult;
                }

                existItem.Pn = dtoItem.Pn;
                existItem.CustId = dtoItem.CustId;
                existItem.Qty = dtoItem.Qty;
                existItem.Status = dtoItem.Status;
                existItem.Creator = dtoItem.Creator;                
                existItem.Udt = this._dateTimeService.GetCurrentTime();
                await this._salesOrderRepository.UpdateAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{updateItem.SO} {SalesOrderConstant.SalesOrderUpdateOK}";
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<SalesOrderEntity>> GetSingleItemAsync(string so)
        {
            ServiceResultModel<SalesOrderEntity> serviceResult = new ServiceResultModel<SalesOrderEntity>();
            try
            {
                SalesOrderEntity existItem = await this._salesOrderRepository.GetSingleItemAsync(so);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{so} {SalesOrderConstant.SalesOrderQueryDataNotFound}";
                    serviceResult.Data = new List<SalesOrderEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderQueryOK;
                serviceResult.Data = new List<SalesOrderEntity>() { existItem };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<SalesOrderEntity>();
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<SalesOrderEntity>> GetAllItemsAsync()
        {
            ServiceResultModel<SalesOrderEntity> serviceResult = new ServiceResultModel<SalesOrderEntity>();
            try
            {
                IEnumerable<SalesOrderEntity> existItems = await this._salesOrderRepository.GetAllItemsAsync();
                if (existItems == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{SalesOrderConstant.SalesOrderQueryDataNotFound}";
                    serviceResult.Data = new List<SalesOrderEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderQueryOK;
                serviceResult.Data = existItems.ToList();
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<SalesOrderEntity>();
                return serviceResult;
            }
        }

        private SalesOrderEntity ConvertSalesOrderEntity(SalesOrderDto dtoItem)
        {
            SalesOrderEntity dbItem = new SalesOrderEntity();
            dbItem.SO = dtoItem.SO;
            dbItem.Pn = dtoItem.Pn;
            dbItem.CustId = dtoItem.CustId;
            dbItem.Qty = dtoItem.Qty;
            dbItem.Status = dtoItem.Status;
            dbItem.Creator = dtoItem.Creator;            
            dbItem.Cdt = _dateTimeService.GetCurrentTime();
            dbItem.Udt = _dateTimeService.GetCurrentTime();
            return dbItem;
        }
    }
}
