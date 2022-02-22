using KTA.Data.Entity;
using KTA.Data.Service;
using KTA.Model.Constants;
using KTA.Model.Entities;
using KTA.Model.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ICustomerRepository _customerRepository;
        private readonly HttpClient _httpClient;
        private readonly Uri _baseCustomerUrl;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseCustomerUrl = httpClient.BaseAddress;
        }
        public CustomerService(ICustomerRepository customerRepository, IDateTimeService dateTimeService)
        {
            _customerRepository = customerRepository;
            _dateTimeService = dateTimeService;

        }

        public async Task<ServiceResultModel<string>> AddAsync(CustomerDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var addItem = this.ConvertCustomerEntity(dtoItem);
                CustomerEntity existItem = await this._customerRepository.GetSingleItemAsync(addItem);
                if (existItem != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = CustomerConstant.CustomerExisted;
                    return serviceResult;
                }

                await this._customerRepository.AddAsync(addItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = CustomerConstant.CustomerInsertOK;
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> DeleteAsync(CustomerDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var deleteItem = this.ConvertCustomerEntity(dtoItem);
                CustomerEntity existItem = await this._customerRepository.GetSingleItemAsync(deleteItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{deleteItem.CustId} {CustomerConstant.CustomerDeleteDataNotFound}";
                    return serviceResult;
                }

                await this._customerRepository.DeleteAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{deleteItem.CustId} {CustomerConstant.CustomerDeleteOK}";
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> UpdateAsync(CustomerDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var updateItem = this.ConvertCustomerEntity(dtoItem);
                CustomerEntity existItem = await this._customerRepository.GetSingleItemAsync(updateItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{dtoItem.CustId} {CustomerConstant.CustomerUpdateDataNotFound}";
                    return serviceResult;
                }

                existItem.Name = dtoItem.Name;
                existItem.Title = dtoItem.Title;
                existItem.Address = dtoItem.Address;
                existItem.Phone = dtoItem.Phone;                           
                existItem.Udt = this._dateTimeService.GetCurrentTime();
                await this._customerRepository.UpdateAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{updateItem.CustId} {CustomerConstant.CustomerUpdateOK}";
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<CustomerEntity>> GetSingleItemAsync(string custId)
        {
            ServiceResultModel<CustomerEntity> serviceResult = new ServiceResultModel<CustomerEntity>();
            try
            {
                CustomerEntity existItem = await this._customerRepository.GetSingleItemAsync(custId);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{custId} {CustomerConstant.CustomerQueryDataNotFound}";
                    serviceResult.Data = new List<CustomerEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = CustomerConstant.CustomerQueryOK;
                serviceResult.Data = new List<CustomerEntity>() { existItem };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<CustomerEntity>();
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<CustomerEntity>> GetAllItemsAsync()
        {
            ServiceResultModel<CustomerEntity> serviceResult = new ServiceResultModel<CustomerEntity>();
            try
            {
                IEnumerable<CustomerEntity> existItems = await this._customerRepository.GetAllItemsAsync();
                if (existItems == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{CustomerConstant.CustomerQueryDataNotFound}";
                    serviceResult.Data = new List<CustomerEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = CustomerConstant.CustomerQueryOK;
                serviceResult.Data = existItems.ToList();
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<CustomerEntity>();
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<DummyCustomerDto>> GetDummyCustomers(string id)
        {
            ServiceResultModel<DummyCustomerDto> serviceResult = new ServiceResultModel<DummyCustomerDto>();
            try
            {                
                string uri = $"{this._baseCustomerUrl}{id}";
                var responseString = await _httpClient.GetStringAsync(uri);
                DummyCustomerDto dummyCust = JsonConvert.DeserializeObject<DummyCustomerDto>(responseString);
                serviceResult.IsSuccess = true;
                serviceResult.Data = new List<DummyCustomerDto>() { dummyCust };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;                
            }
        }

        private CustomerEntity ConvertCustomerEntity(CustomerDto dtoItem)
        {
            CustomerEntity dbItem = new CustomerEntity();
            dbItem.CustId = dtoItem.CustId;
            dbItem.Name = dtoItem.Name;
            dbItem.Title = dtoItem.Title;
            dbItem.Address = dtoItem.Address;
            dbItem.Phone = dtoItem.Phone;                 
            dbItem.Cdt = _dateTimeService.GetCurrentTime();
            dbItem.Udt = _dateTimeService.GetCurrentTime();
            return dbItem;
        }
    }
}
