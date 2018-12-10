using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CML.APICaller
{
    public class BaseClient
    {
        private static object _locker = new object();
        private static volatile HttpClient _client;
        public static string _token { private get; set; }

        protected static HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (_locker)
                    {
                        if (_client == null)
                        {
                            _client = new HttpClient();
                            _client.BaseAddress = new Uri(ProjectConstants.APIURL);
                            _client.DefaultRequestHeaders.Accept.Clear();
                            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            if(!string.IsNullOrEmpty(_token))
                            {
                                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                            }
                        }
                    }
                }
                return _client;
            }
        }
       
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_client != null)
                {
                    _client.Dispose();
                }

                _client = null;
            }
        }
    }
}
