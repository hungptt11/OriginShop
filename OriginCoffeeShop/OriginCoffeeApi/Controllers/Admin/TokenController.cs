using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CML.Business;
using CML.Models.Model;
using CML.Helper.Utilities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OriginCoffeeApi.Controllers.Admin
{
    [AllowAnonymous]
    [RoutePrefix("api/GetLoginToken")]
    public class TokenController : OriginBaseApi {

        IBaseBusiness<UserToken> _iBaseBusiness;
        public TokenController()
        {
            _iBaseBusiness = new BaseBusiness<UserToken>();
        }
        // GET api/<controller>/5
        [HttpGet]        
        [Route("GetToken")]
        public async Task<IHttpActionResult> Get([FromUri] string userName)
        {            
            UserToken token = null;
            await Task.Run(() => {
                Expression<Func<UserToken, bool>> predicate = x => x.UserName == userName;
                token  = _iBaseBusiness.GetItem(predicate);
            });            
            if (token.IsNull())
            {
                return NotFound();
            }
            return Ok(token.Token);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("PostToken")]
        public async Task<IHttpActionResult> Post([FromBody]UserToken model)
        {
            bool result = true;
            if(ModelState.IsValid)
            {
                await Task.Run(() => result = _iBaseBusiness.Insert(model));
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("PutToken")]
        public async Task<IHttpActionResult> Put([FromUri] string userName)
        {
            if (ModelState.IsValid)
            {
                string sql = "update UserToken set Token = @TokenValue  where UserName = @username";
                Random rand = new Random();
                SqlParameter param1 = new SqlParameter("username", userName);
                SqlParameter param2 = new SqlParameter("TokenValue", rand.Next(1000,9999));
                if (await _iBaseBusiness.ExecuteSqlCommandQuery(sql, param1, param2) > 0)
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);            
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string userName)
        {
            string sql = "delete from UserToken where UserName = @username";
            SqlParameter param = new SqlParameter ("username",userName);
            if (await _iBaseBusiness.ExecuteSqlCommandQuery(sql, param) > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}