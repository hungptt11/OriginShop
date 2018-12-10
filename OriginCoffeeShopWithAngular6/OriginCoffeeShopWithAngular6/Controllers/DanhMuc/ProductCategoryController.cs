using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CML.NetCore.Models.Models;
using CML.NetCore.Services.DanhMuc;
using Microsoft.Extensions.Logging;

namespace OriginCoffeeShopWithAngular6.Controllers.DanhMuc
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService<ProductCategory> _IProductCategoryService;
        private readonly ILogger<ProductCategoryController> _logger;
        public ProductCategoryController(IProductCategoryService<ProductCategory> iProductCategoryService, ILogger<ProductCategoryController> logger)
        {
            _IProductCategoryService = iProductCategoryService;
            _logger = logger;
        }
        [HttpGet("[action]")]
        public List<ProductCategory> ProductCategoriesList()
        {
            try
            {
                var result = _IProductCategoryService.GetList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{0}:  public List<ProductCategory> ProductCategoriesList()", this.GetType().ToString());
            }
            return null;
        }
        [HttpPost("[action]")]
        public bool AddEntity(ProductCategory product)
        {
            try
            {
                var result = _IProductCategoryService.Insert(product);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{0}:  public List<ProductCategory> ProductCategoriesList()", this.GetType().ToString());
            }
            return false;
        }
        [HttpPut("[action]")]
        public bool EditEntity(ProductCategory product)
        {
            try
            {
                var result = _IProductCategoryService.Edit(product);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{0}:  public List<ProductCategory> ProductCategoriesList()", this.GetType().ToString());
            }
            return false;
        }
        [HttpDelete("[action]")]
        public bool DeleteEntity([FromQuery] string id)
        {
            try
            {
                var result = _IProductCategoryService.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{0}:  public List<ProductCategory> ProductCategoriesList()", this.GetType().ToString());
            }
            return false;
        }
    }
}