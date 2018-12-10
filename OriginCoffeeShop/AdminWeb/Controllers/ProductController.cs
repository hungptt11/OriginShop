using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CML.Helper.Utilities;
using ProjectConstant;
using CML.APICaller.DanhMuc;
using System.Threading.Tasks;
using CML.Models.Model;

namespace AdminWeb.Controllers
{
    public class ProductController : BaseController
    {
        // GET: ProductCategories
        public ActionResult Index()
        {
            var isAccessAvaiable = IsAccessAvaiable();
            if (isAccessAvaiable.IsNull())
            {
                return RedirectToAction("login", "login", new { returnUrl = "/ProductCategories/Index" });
            }
            ProductCategoriesCaller._token = isAccessAvaiable;
            return View();
        }

        public async Task<JsonResult> List()
        {
            var isAccessAvaiable = IsAccessAvaiable();
            if (isAccessAvaiable.IsNull())
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.doesnothavepermission }, JsonRequestBehavior.AllowGet);
            }
            var result = await ProductCategoriesCaller.GetListProductCategories("ProductCategories/GetList", isAccessAvaiable);
            if (result == null)
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.RecordNotFound }, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Add(ProductCategories ngl)
        {
            var isAccessAvaiable = IsAccessAvaiable();
            if (isAccessAvaiable.IsNull())
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.doesnothavepermission }, JsonRequestBehavior.AllowGet);
            }
            //if (CheckKey(ngl.Name))
            //{
            //    return Json(new { Return_Cd = false, ErrorMess = MessageContant.DuplicateRecord }, JsonRequestBehavior.AllowGet);
            //}
            var result = await ProductCategoriesCaller.PostProductCategory("ProductCategories/AddNewRecord",ngl,isAccessAvaiable);
            if(result)
            {
                return Json(new { Return_Cd = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.InsertFail }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetbyID(int ID)
        {
            var isAccessAvaiable = IsAccessAvaiable();
            if (isAccessAvaiable.IsNull())
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.doesnothavepermission }, JsonRequestBehavior.AllowGet);
            }
            var result = await ProductCategoriesCaller.GetProductCategory("ProductCategories/GetByID/" + ID, isAccessAvaiable);
            if (result == null)
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.RecordNotFound }, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(ProductCategories ngl)
        {
            var isAccessAvaiable = IsAccessAvaiable();
            if (isAccessAvaiable.IsNull())
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.doesnothavepermission }, JsonRequestBehavior.AllowGet);
            }
            var result = await ProductCategoriesCaller.PutProductCategory("ProductCategories/EditRecord", ngl, isAccessAvaiable);
            if(result)
            {
                return Json(new { Return_Cd = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.InsertFail }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> Delete(int ID)
        {
            var isAccessAvaiable = IsAccessAvaiable();
            if (isAccessAvaiable.IsNull())
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.doesnothavepermission }, JsonRequestBehavior.AllowGet);
            }
            var result = await ProductCategoriesCaller.DeleteProductCategory("ProductCategories/DeleteRecord/" + ID, isAccessAvaiable);
            if (result == false)
            {
                return Json(new { Return_Cd = false, ErrorMess = MessageContant.RecordNotFound }, JsonRequestBehavior.AllowGet);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //[NonAction]
        //private bool CheckKey(String Name)
        //{
        //    List<String> lst = (from item in nguyeLieuBusiness.GetList<ThanhPhamCafe>() select item.TenThanhPham).ToList();
        //    if (lst.Count != 0)
        //    {
        //        if (lst.Contains(Name))
        //            return true;
        //    }
        //    return false;
        //}
        
    }
}