using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CML.Business;
using CML.Models.Model;
using CML.Helper.Utilities;
using ProjectConstant;

namespace OriginCoffeeApi.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("api/ProductCategories")]
    public class ProductCategoriesController : OriginBaseApi {

        IProductCategoriesBusiness<ProductCategories> _iProductCategoriesBusiness;
        public ProductCategoriesController(IProductCategoriesBusiness<ProductCategories> iProductCategoriesBusiness) 
        {
            _iProductCategoriesBusiness = iProductCategoriesBusiness;
        }
        // GET api/<controller>
        [HttpGet]
        [Route("GetList")]
        public List<ProductCategories> Get()
        {
            return CacheManagement.GetListProductCategories();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetByID")]
        public IHttpActionResult Get(int id)
        {
            var product = CacheManagement.GetListProductCategories().SingleOrDefault(i => i.Id == id);
            if(product.IsNull())
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddNewRecord")]
        public IHttpActionResult Post(ProductCategories model)
        {
            if(ModelState.IsValid)
            {
                if(_iProductCategoriesBusiness.Insert(model))
                {
                    CacheManagement.RemoveProducts();
                    return Ok(1);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("EditRecord")]
        public IHttpActionResult Put(ProductCategories model)
        {
            if (ModelState.IsValid)
            {
                if (_iProductCategoriesBusiness.Edit(model))
                {
                    CacheManagement.RemoveProducts();
                    return Ok(1);
                }
                return NotFound();
            }
            return BadRequest(ModelState);            
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("DeleteRecord")]
        public IHttpActionResult Delete(int id)
        {
            if(CacheManagement.GetListProducts().Where(pd => pd.CategoryID == id).Count() > 0)
            {
                return Json(new { error = true, errormessage = MessageContant.Recordisused });
            }
            if (_iProductCategoriesBusiness.Delete(id))
            {
                
                CacheManagement.RemoveProducts();
                return Ok(1);
            }
            return NotFound();
        }
    }
}