using KingsTeaApp.Filter;
using KTA.Data.Entity;
using KTA.Model.Constants;
using KTA.Model.Entities;
using KTA.Model.Interface;
using KTA.Model.Services;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost, Route("AddProductAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> AddProductAsync(ProductDto addProductDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._productService.AddAsync(addProductDto);
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

        [HttpPut, Route("UpdateProductAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> UpdateProductAsync(ProductDto updateProductDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._productService.UpdateAsync(updateProductDto);
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

        [HttpDelete, Route("DeleteProductAsync")]
        [ValidateModel]
        public async Task<ApiResultModel<string>> DeleteProductAsync(ProductDto deleteProductDto)
        {
            ApiResultModel<string> result = new ApiResultModel<string>();
            try
            {
                ServiceResultModel<string> serviceResult = await this._productService.DeleteAsync(deleteProductDto);
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

        [HttpGet, Route("GetSingleProductAsync")]
        public async Task<ApiResultModel<ProductEntity>> GetSingleProductAsync(string pn)
        {
            ApiResultModel<ProductEntity> result = new ApiResultModel<ProductEntity>();
            try
            {
                ServiceResultModel<ProductEntity> serviceResult = await this._productService.GetSingleItemAsync(pn);
                if (!serviceResult.IsSuccess)
                {
                    // service exception
                    result.IsSuccess = serviceResult.IsSuccess;
                    result.Message = serviceResult.Message;
                    return result;
                }

                result.IsSuccess = serviceResult.IsSuccess;
                result.Message = serviceResult.Message;
                result.Data = new List<ProductEntity>() { serviceResult.Data.FirstOrDefault() };
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message + ex.StackTrace;
                return result;
            }
        }

        [HttpGet, Route("GetAllProductsAsync")]
        public async Task<ApiResultModel<ProductEntity>> GetAllProductsAsync()
        {
            ApiResultModel<ProductEntity> result = new ApiResultModel<ProductEntity>();
            try
            {
                ServiceResultModel<ProductEntity> serviceResult = await this._productService.GetAllItemsAsync();
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
