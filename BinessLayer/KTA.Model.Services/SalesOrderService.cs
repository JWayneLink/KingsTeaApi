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
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly string _prefixKTA = "KTA-";

        public SalesOrderService(ISalesOrderRepository salesOrderRepository, ICustomerService customerService, IProductService productService, IDateTimeService dateTimeService)
        {
            _salesOrderRepository = salesOrderRepository;
            _productService = productService;
            _customerService = customerService;            
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
                serviceResult.Data = new List<string>() { addItem.SO };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> AddBulkAsync(List<SalesOrderDto> dtoItems)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var addItems = this.ConvertSalesOrderBulkEntity(dtoItems);
               await this._salesOrderRepository.AddBulkAsync(addItems);

                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderInsertOK;
                serviceResult.Data = new List<string>() { addItems.First().SO };
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
                IEnumerable<SalesOrderEntity> existItems = await this._salesOrderRepository.GetSingleItemAsync(so);
                if (existItems == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{so} {SalesOrderConstant.SalesOrderQueryDataNotFound}";
                    serviceResult.Data = new List<SalesOrderEntity>();
                    return serviceResult;
                }

                List<SalesOrderEntity> data = new List<SalesOrderEntity>();
                data.AddRange(existItems.ToList());
                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderQueryOK;
                serviceResult.Data = data;
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
                if (existItems == null || existItems.ToList().Count == 0)
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

        public async Task<ServiceResultModel<SalesOrderDetailDto>> GetAllItemsDetailAsync()
        {
            ServiceResultModel<SalesOrderDetailDto> serviceResult = new ServiceResultModel<SalesOrderDetailDto>();
            try
            {
                IEnumerable<SalesOrderEntity> existItems = await this._salesOrderRepository.GetAllItemsAsync();
                if (existItems == null || existItems.ToList().Count == 0)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{SalesOrderConstant.SalesOrderQueryDataNotFound}";
                    serviceResult.Data = new List<SalesOrderDetailDto>();
                    return serviceResult;
                }

                // 訂單對客戶是一對一
                // 訂單對產品是一對多
                List<SalesOrderDetailDto> salesOrderDetailDtos = new List<SalesOrderDetailDto>();
                                
                var soGroups = existItems.GroupBy(x => new { x.SO, x.CustId, x.Status, x.Creator }).ToList();
                foreach (var soGroup in soGroups)
                {
                    SalesOrderDetailDto salesOrderDetailDto = new SalesOrderDetailDto();
                    salesOrderDetailDto.SO = soGroup.Key.SO;                    
                    salesOrderDetailDto.Status = soGroup.Key.Status;
                    salesOrderDetailDto.Creator = soGroup.Key.Creator;

                    ServiceResultModel<CustomerEntity> custInfo = await this._customerService.GetSingleItemAsync(soGroup.Key.CustId);
                    if (custInfo.Data.Count == 0)
                    {
                        serviceResult.IsSuccess = false;
                        serviceResult.Message = $"{soGroup.Key.SO} could not find related Customer information.";
                        serviceResult.Data = new List<SalesOrderDetailDto>();
                        return serviceResult;
                    }
                    salesOrderDetailDto.CustName = custInfo.Data.First().Name;
                    salesOrderDetailDto.CustTitle = custInfo.Data.First().Title;
                    salesOrderDetailDto.CustAddress = custInfo.Data.First().Address;
                    salesOrderDetailDto.CustPhone = custInfo.Data.First().Phone;

                    List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();

                    decimal totaPrice = 0;
                    foreach (var so in soGroup)
                    {
                        OrderDetailDto orderDetailDto = new OrderDetailDto();
                        ServiceResultModel<ProductEntity> prodInfo = await this._productService.GetSingleItemAsync(so.Pn);

                        if (prodInfo.Data.Count == 0)
                        {
                            serviceResult.IsSuccess = false;
                            serviceResult.Message = $"{soGroup.Key.SO} could not find related Product information.";
                            serviceResult.Data = new List<SalesOrderDetailDto>();
                            return serviceResult;
                        }
                        orderDetailDto.ProductName = prodInfo.Data.First().Name;
                        orderDetailDto.ProductCategory = prodInfo.Data.First().Category;
                        orderDetailDto.ProductSize = prodInfo.Data.First().Size;
                        orderDetailDto.ProductSugar = prodInfo.Data.First().Sugar;
                        orderDetailDto.ProductIce = prodInfo.Data.First().Ice;
                        orderDetailDto.ProductPrice = prodInfo.Data.First().Price;
                        orderDetailDto.ProductQty = so.Qty;
                        orderDetailDto.ProductSubTotal = prodInfo.Data.First().Price * so.Qty;
                        totaPrice += orderDetailDto.ProductSubTotal;
                        salesOrderDetailDto.Cdt = so.Cdt;
                        salesOrderDetailDto.Udt = so.Udt;

                        orderDetailDtos.Add(orderDetailDto);
                    }
                    salesOrderDetailDto.TotalPrice = totaPrice;
                    salesOrderDetailDto.OrderDetailDtos = orderDetailDtos;
                    salesOrderDetailDtos.Add(salesOrderDetailDto);
                }                

                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderQueryOK;
                serviceResult.Data = salesOrderDetailDtos;
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<SalesOrderDetailDto>();
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<SalesOrderDetailDto>> GetAllItemsDetailAsync(string so)
        {
            ServiceResultModel<SalesOrderDetailDto> serviceResult = new ServiceResultModel<SalesOrderDetailDto>();
            try
            {
                IEnumerable<SalesOrderEntity> existItems = await this._salesOrderRepository.GetSingleItemAsync(so);
                if (existItems == null || existItems.ToList().Count == 0)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{SalesOrderConstant.SalesOrderQueryDataNotFound}";
                    serviceResult.Data = new List<SalesOrderDetailDto>();
                    return serviceResult;
                }

                // 訂單對客戶是一對一
                // 訂單對產品是一對多
                List<SalesOrderDetailDto> salesOrderDetailDtos = new List<SalesOrderDetailDto>();

                var soGroups = existItems.GroupBy(x => new { x.SO, x.CustId, x.Status, x.Creator }).ToList();
                foreach (var soGroup in soGroups)
                {
                    SalesOrderDetailDto salesOrderDetailDto = new SalesOrderDetailDto();
                    salesOrderDetailDto.SO = soGroup.Key.SO;
                    salesOrderDetailDto.Status = soGroup.Key.Status;
                    salesOrderDetailDto.Creator = soGroup.Key.Creator;

                    ServiceResultModel<CustomerEntity> custInfo = await this._customerService.GetSingleItemAsync(soGroup.Key.CustId);
                    if (custInfo.Data.Count == 0)
                    {
                        serviceResult.IsSuccess = false;
                        serviceResult.Message = $"{soGroup.Key.SO} could not find related Customer information.";
                        serviceResult.Data = new List<SalesOrderDetailDto>();
                        return serviceResult;
                    }
                    salesOrderDetailDto.CustName = custInfo.Data.First().Name;
                    salesOrderDetailDto.CustTitle = custInfo.Data.First().Title;
                    salesOrderDetailDto.CustAddress = custInfo.Data.First().Address;
                    salesOrderDetailDto.CustPhone = custInfo.Data.First().Phone;

                    List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();

                    decimal totaPrice = 0;
                    foreach (var s in soGroup)
                    {
                        OrderDetailDto orderDetailDto = new OrderDetailDto();
                        ServiceResultModel<ProductEntity> prodInfo = await this._productService.GetSingleItemAsync(s.Pn);

                        if (prodInfo.Data.Count == 0)
                        {
                            serviceResult.IsSuccess = false;
                            serviceResult.Message = $"{soGroup.Key.SO} could not find related Product information.";
                            serviceResult.Data = new List<SalesOrderDetailDto>();
                            return serviceResult;
                        }
                        orderDetailDto.ProductName = prodInfo.Data.First().Name;
                        orderDetailDto.ProductCategory = prodInfo.Data.First().Category;
                        orderDetailDto.ProductSize = prodInfo.Data.First().Size;
                        orderDetailDto.ProductSugar = prodInfo.Data.First().Sugar;
                        orderDetailDto.ProductIce = prodInfo.Data.First().Ice;
                        orderDetailDto.ProductPrice = prodInfo.Data.First().Price;
                        orderDetailDto.ProductQty = s.Qty;
                        orderDetailDto.ProductSubTotal = prodInfo.Data.First().Price * s.Qty;
                        totaPrice += orderDetailDto.ProductSubTotal;
                        salesOrderDetailDto.Cdt = s.Cdt;
                        salesOrderDetailDto.Udt = s.Udt;

                        orderDetailDtos.Add(orderDetailDto);
                    }
                    salesOrderDetailDto.TotalPrice = totaPrice;
                    salesOrderDetailDto.OrderDetailDtos = orderDetailDtos;
                    salesOrderDetailDtos.Add(salesOrderDetailDto);
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderQueryOK;
                serviceResult.Data = salesOrderDetailDtos;
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<SalesOrderDetailDto>();
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> GetSalesOrderListAsync(string so)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                List<string> soItems = await this._salesOrderRepository.GetSalesOrderListAsync(so);
                if (soItems == null || soItems.ToList().Count == 0)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{SalesOrderConstant.SalesOrderQueryDataNotFound}";
                    serviceResult.Data = new List<string>();
                    return serviceResult;
                }

                var groupSo = soItems.GroupBy(x => x).Select(x=>x.Key).ToList();
                serviceResult.IsSuccess = true;
                serviceResult.Message = SalesOrderConstant.SalesOrderQueryOK;
                serviceResult.Data = groupSo;
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

        private SalesOrderEntity ConvertSalesOrderEntity(SalesOrderDto dtoItem)
        {

            SalesOrderEntity dbItem = new SalesOrderEntity();
            var timestamp = this._dateTimeService.GetCurrentTime().ToString("yyyyMMddHHmmssfff");
            dbItem.SO = $"{this._prefixKTA}{timestamp}";
            dbItem.Pn = dtoItem.Pn;
            dbItem.CustId = dtoItem.CustId;
            dbItem.Qty = dtoItem.Qty;
            dbItem.Status = dtoItem.Status;
            dbItem.Creator = dtoItem.Creator;            
            dbItem.Cdt = _dateTimeService.GetCurrentTime();
            dbItem.Udt = _dateTimeService.GetCurrentTime();
            return dbItem;
        }

        private List<SalesOrderEntity> ConvertSalesOrderBulkEntity(List<SalesOrderDto> dtoItems)
        {
            List<SalesOrderEntity> dbItems = new List<SalesOrderEntity>();
            var timestamp = this._dateTimeService.GetCurrentTime().ToString("yyyyMMddHHmmssfff");
            foreach (var dtoItem in dtoItems)
            {
                SalesOrderEntity dbItem = new SalesOrderEntity();
                dbItem.SO = $"{this._prefixKTA}{timestamp}";
                dbItem.Pn = dtoItem.Pn;
                dbItem.CustId = dtoItem.CustId;
                dbItem.Qty = dtoItem.Qty;
                dbItem.Status = dtoItem.Status;
                dbItem.Creator = dtoItem.Creator;
                dbItem.Cdt = _dateTimeService.GetCurrentTime();
                dbItem.Udt = _dateTimeService.GetCurrentTime();
                dbItems.Add(dbItem);
            }
            return dbItems;
        }
    }
}
