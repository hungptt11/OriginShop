using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWeb.Controllers
{
    public class BaseController : Controller
    {
        protected HttpCookie CreateCookie(string cookiesName, string value, DateTime dt)
        {
            HttpCookie cookies = new HttpCookie(cookiesName);
            cookies.Value = value;
            cookies.Expires = dt;
            return cookies;
        }
        protected string IsAccessAvaiable()
        {
            var cookies = Request.Cookies.Get("access_token");
            if(cookies == null)
            {
                return string.Empty;
            }
            return cookies.Value;
        }
    }
}