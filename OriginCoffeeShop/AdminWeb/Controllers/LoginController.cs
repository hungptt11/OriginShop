using AdminWeb.Models;
using CML.Helper.Core;
using CML.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CML.Helper.Utilities;
using CML.APICaller.DanhMuc;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AdminWeb.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController()
        {
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserModel model, string ReturnUrl)
        {
            var token = model.Token;
            if (token.IsNotNull())
            {
                string Value = await TokenCaller.GetAPIToken(model.Account, model.Password);
                if (Value.IsNotNull())
                {
                    //if (Membership.ValidateUser(model.Account, model.Password))
                    //{                       
                    //    if (baseBusiness.ExecuteSqlCommandQuery<UserToken>(sqlcommand) > 0)
                    //    {
                    //        FormsAuthentication.SetAuthCookie(model.Account, false);                    
                    //    }
                    //}\

                    var jObject = JObject.Parse(Value);
                    Response.Cookies.Add(CreateCookie("access_token", jObject.GetValue("access_token").ToString(), DateTime.Parse(jObject.GetValue(".expires").ToString())));
                    await TokenCaller.PutLoginToken("GetLoginToken/PutToken?userName=hungpt11");
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }
        public ActionResult SignOut()
        {
            //FormsAuthentication.SignOut();
            //var sqlcommand = string.Format("update UserOrigin set LoginStatus = 0 where Account ='{0}'", this.User.Identity.Name);
            //new BaseBusiness().ExecuteSqlCommandQuery<UserToken>(sqlcommand);
            //return RedirectToAction("Login", "Login");
            return View();
        }
        [HttpGet]
        public ActionResult GetToken()
        {
            return View();
        }
        public async Task<JsonResult> TokenResult()
        {
            var username = Request["username"];
            if (string.IsNullOrEmpty(username))
            {
                return Json(new { Return_Cd = false, ErrorMess = "Vui lòng nhập username hợp lệ" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userToken = await TokenCaller.GetLoginToken("GetLoginToken/GetToken?userName=hungpt11");
                if (userToken == 0)
                {
                    return Json(new { Return_Cd = false, ErrorMess = "Token không tồn tại, vui lòng liên hệ system admin" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { Return_Cd = true, ErrorMess = userToken }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}