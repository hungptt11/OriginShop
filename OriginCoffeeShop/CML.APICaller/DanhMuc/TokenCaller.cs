using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CML.APICaller.DanhMuc
{
    public class TokenCaller : BaseClient
    {
        /// <summary>
        /// Get token for login in API
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<string> GetAPIToken(string userName, string password)
        {          
                //setup client
                //Client.BaseAddress = new Uri(ProjectConstants.APIURL);
                //Client.DefaultRequestHeaders.Accept.Clear();
                //Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 //var request = new HttpRequestMessage(HttpMethod.Post, url);
                 //request.Headers.Accept.Clear();
                 //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                 //request.Content = new StringContent("{...}", Encoding.UTF8, "application/json");
                //setup login data                
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"), 
                    new KeyValuePair<string, string>("username", userName), 
                    new KeyValuePair<string, string>("password", password), 
                });
                //send request
                HttpResponseMessage responseMessage = await Client.PostAsync("/Token", formContent);

                //get access token from response body
                var responseJson = await responseMessage.Content.ReadAsStringAsync();
                //var jObject = JObject.Parse(responseJson);
                //return jObject.GetValue("access_token").ToString();
                return responseJson;
        }
        public static async Task<int> PutLoginToken(string router)
        {
            //setup client            
            //Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //send request;
            try
            {
                HttpResponseMessage response = await Client.PutAsync(router,null);
                var responseString = await response.Content.ReadAsStringAsync();
                return int.Parse(responseString);               
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Get token to login in Admin page
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static async Task<int> GetLoginToken(string router)
        {
            //setup client            
            //Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //send request;
            try
            {
                HttpResponseMessage response = await Client.GetAsync(router);
                var responseString = await response.Content.ReadAsStringAsync();
                return int.Parse(responseString);
               // https://stackoverflow.com/questions/42235677/httpclient-this-instance-has-already-started
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
