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
    public class ProductService : IProductService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IDateTimeService dateTimeService)
        {
            _productRepository = productRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<ServiceResultModel<string>> AddAsync(ProductDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var addItem = this.ConvertProductEntity(dtoItem);
                ProductEntity existItem = await this._productRepository.GetSingleItemAsync(addItem);
                if (existItem != null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = ProductConstant.ProductExisted;
                    return serviceResult;
                }

                await this._productRepository.AddAsync(addItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = ProductConstant.ProductInsertOK;
                return serviceResult;                
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;                
            }
        }

        public async Task<ServiceResultModel<string>> DeleteAsync(ProductDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var deleteItem = this.ConvertProductEntity(dtoItem);
                ProductEntity existItem = await this._productRepository.GetSingleItemAsync(deleteItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{deleteItem.Pn} {ProductConstant.ProductDeleteDataNotFound}";
                    return serviceResult;
                }

                await this._productRepository.DeleteAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{deleteItem.Pn} {ProductConstant.ProductDeleteOK}";
                return serviceResult;                
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;
            }
        }

        public async Task<ServiceResultModel<string>> UpdateAsync(ProductDto dtoItem)
        {
            ServiceResultModel<string> serviceResult = new ServiceResultModel<string>();
            try
            {
                var updateItem = this.ConvertProductEntity(dtoItem);
                ProductEntity existItem = await this._productRepository.GetSingleItemAsync(updateItem);
                if (existItem == null)
                {
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{dtoItem.Pn} {ProductConstant.ProductUpdateDataNotFound}";
                    return serviceResult;
                }

                existItem.Name = dtoItem.Name;
                existItem.Category = dtoItem.Category;
                existItem.Size = dtoItem.Size;
                existItem.Price = dtoItem.Price;
                existItem.Kcal = dtoItem.Kcal;
                existItem.COO = dtoItem.COO;
                existItem.Udt = this._dateTimeService.GetCurrentTime();
                await this._productRepository.UpdateAsync(existItem);
                serviceResult.IsSuccess = true;
                serviceResult.Message = $"{updateItem.Pn} {ProductConstant.ProductUpdateOK}";
                return serviceResult;                
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = ex.Message + ex.StackTrace;
                return serviceResult;                
            }
        }

        public async Task<ServiceResultModel<ProductEntity>> GetSingleItemAsync(string pn)
        {
            ServiceResultModel<ProductEntity> serviceResult = new ServiceResultModel<ProductEntity>();
            try
            {
                ProductEntity existItem = await this._productRepository.GetSingleItemAsync(pn);
                if (existItem == null)
                {                    
                    serviceResult.IsSuccess = true;
                    serviceResult.Message = $"{pn} {ProductConstant.ProductQueryDataNotFound}";
                    serviceResult.Data = new List<ProductEntity>();
                    return serviceResult;
                }

                serviceResult.IsSuccess = true;
                serviceResult.Message = ProductConstant.ProductQueryOK;
                serviceResult.Data = new List<ProductEntity>() { existItem };
                return serviceResult;
            }
            catch (Exception ex)
            {
                serviceResult.IsSuccess = false;
                serviceResult.Message = $"{ex.Message} {ex.StackTrace}";
                serviceResult.Data = new List<ProductEntity>();
                return serviceResult;
            }
        }   

        private ProductEntity ConvertProductEntity(ProductDto dtoItem)
        {
            ProductEntity dbItem = new ProductEntity();
            dbItem.Pn = dtoItem.Pn;
            dbItem.Name = dtoItem.Name;
            dbItem.Category = dtoItem.Category;
            dbItem.Size = dtoItem.Size;
            dbItem.Price = dtoItem.Price;
            dbItem.Kcal = dtoItem.Kcal;
            dbItem.COO = dtoItem.COO;
            dbItem.Cdt = _dateTimeService.GetCurrentTime();
            dbItem.Udt = _dateTimeService.GetCurrentTime();
            return dbItem;
        }
    }
}
