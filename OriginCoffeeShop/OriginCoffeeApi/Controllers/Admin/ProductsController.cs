using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CML.Business;
using CML.Models.Model;
using CML.Helper.Utilities;

namespace OriginCoffeeApi.Controllers.Admin
{
    [Authorize]
    [RoutePrefix("api/Products")]
    public class ProductsController : OriginBaseApi {
    
        IProductsBusiness<Products> _iProductsBusiness;
        public ProductsController(IProductsBusiness<Products> iProductsBusiness) 
        {
            _iProductsBusiness = iProductsBusiness;
        }
        public ProductsController()
        {
            _iProductsBusiness = new ProductsBusiness<Products>();
        }
        // GET api/<controller>
        [HttpGet]
        [Route("GetList")]
        public List<Products> Get()
        {
            return CacheManagement.GetListProducts();
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(int id)
        {
            var product = CacheManagement.GetListProducts().SingleOrDefault(i => i.Id == id);
            if(product.IsNull())
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("PostProduct")]
        public IHttpActionResult Post(Products model)
        {
            if(ModelState.IsValid)
            {
                if(_iProductsBusiness.Insert(model))
                {
                    CacheManagement.RemoveProducts();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("PutProduct")]
        public IHttpActionResult Put(int id, Products model)
        {
            if (ModelState.IsValid && id != model.Id)
            {
                if (_iProductsBusiness.Edit(model))
                {
                    CacheManagement.RemoveProducts();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);            
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("DeleteProduct")]
        public IHttpActionResult Delete(int id)
        {
            if (_iProductsBusiness.Delete(id))
            {
                CacheManagement.RemoveProducts();
                return Ok();
            }
            return NotFound();
        }
    }
}