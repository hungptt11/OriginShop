using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CML.Models.Model;
using Newtonsoft.Json;

namespace CML.APICaller.DanhMuc
{
    public class ProductCategoriesCaller : BaseClient
    {
        /// <summary>
        /// Get List Product Category
        /// </summary>
        /// <param name="router"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<ProductCategories>> GetListProductCategories(string router,string token)
        {
            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(router);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductCategories>>(responseJson);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<bool> PostProductCategory(string router, ProductCategories product, string token)
        {
            try
            {
                HttpResponseMessage responseMessage = await Client.PostAsJsonAsync(router, product);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return bool.Parse(responseJson);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<bool> PutProductCategory(string router, ProductCategories product, string token)
        {
            try
            {
                HttpResponseMessage responseMessage = await Client.PutAsJsonAsync(router, product);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return bool.Parse(responseJson);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static async Task<ProductCategories> GetProductCategory(string router ,string token)
        {
            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(router);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductCategories>(responseJson);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<bool> DeleteProductCategory(string router, string token)
        {
            try
            {
                HttpResponseMessage responseMessage = await Client.GetAsync(router);
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                return bool.Parse(responseJson);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}
