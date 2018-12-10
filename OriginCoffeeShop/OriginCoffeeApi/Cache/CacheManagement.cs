using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CML.Models.Model;
using CML.Business;
using CML.Helper.Core;

namespace OriginCoffeeApi
{
    public class CacheConstant
    {
        public const string ProductsListKey = "ProductsListKey";
        public const string ProductCategoriesListKey = "ProductCategoriesListKey";
    }
    public class CacheManagement
    {
        #region Product
        public static List<Products> GetListProducts()
        {
            if(GlobalCache.Instance.ContainKey(CacheConstant.ProductsListKey))
            {
                return GlobalCache.Instance.GetListItem<Products>(CacheConstant.ProductsListKey);
            }
            else
            {
                GlobalCache.Instance.Add(CacheConstant.ProductsListKey, (new ProductsBusiness<Products>()).GetList());
                return GlobalCache.Instance.GetListItem<Products>(CacheConstant.ProductsListKey);
            }            
        }
        public static void RemoveProducts()
        {
            if (GlobalCache.Instance.ContainKey(CacheConstant.ProductsListKey))
            {
                GlobalCache.Instance.Remove(CacheConstant.ProductsListKey);
            }
        }
        public static List<ProductCategories> GetListProductCategories()
        {
            if (GlobalCache.Instance.ContainKey(CacheConstant.ProductCategoriesListKey))
            {
                return GlobalCache.Instance.GetListItem<ProductCategories>(CacheConstant.ProductCategoriesListKey);
            }
            else
            {
                GlobalCache.Instance.Add(CacheConstant.ProductCategoriesListKey, (new ProductsBusiness<ProductCategories>()).GetList());
                return GlobalCache.Instance.GetListItem<ProductCategories>(CacheConstant.ProductCategoriesListKey);
            }
        }
        public static void RemoveProductCategories()
        {
            if (GlobalCache.Instance.ContainKey(CacheConstant.ProductsListKey))
            {
                GlobalCache.Instance.Remove(CacheConstant.ProductsListKey);
            }
        }
        #endregion
        
    }
}