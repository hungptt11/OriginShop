using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectConstant
{
    public class OriginConstant
    {
        public const string USER_SESSION = "USER_SESSION";
        public const string USER_NAME = "USER_NAME";
        /// <summary>
        /// 0 - BangTin,1- DanhMuc, 3 - SystemAdmin
        /// </summary>
        public static string[] Moduls = new string[] { "BangTin", "DanhMuc", "SystemAdmin" };
    }
}