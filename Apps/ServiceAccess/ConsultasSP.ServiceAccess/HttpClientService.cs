using ConsultasSP.CrossCutting.Dominio.Globales;
using ConsultasSP.CrossCutting.Dominio.ViewModels;
using ConsultasSP.Util.Commom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace ConsultasSP.ServiceAccess
{
    public class HttpClientService : HttpClient
    {
        private static readonly HttpClientService instance = new HttpClientService();

        static HttpClientService() { }

        private HttpClientService() : base()
        {
            Timeout = TimeSpan.FromSeconds(60);
            MaxResponseContentBufferSize = 999999;
        }

        /// <summary>
        /// Returns the singleton instance of HttpClient
        /// </summary>
        public static HttpClientService Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task<List<T>> GetListItems<T>(string Url)
        {
            var uri = new Uri(string.Format(Constantes.BaseUrlApi, Url));
            if (VariablesGlobales.Token != "")
            {
                DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", VariablesGlobales.Token);
            }
            DefaultRequestHeaders.ExpectContinue = false;
            var response = await GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<T>>(content);
                return Items;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<T> GetItem<T>(string Url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, Url));
                if (VariablesGlobales.Token != "")
                {
                    DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", VariablesGlobales.Token);
                }
                var response = await GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<T>(content);
                    return Item;
                }
                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PostItem<T>(T item, string url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, url));
                if (VariablesGlobales.Token != "")
                {
                    DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "1234");
                }
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var errorMessage = JsonConvert.DeserializeObject<ErrorMessage>(contentResponse);
                    throw new Exception(errorMessage.Message);
                }
            }
            catch (Exception ex)
            {
                string errorType = ex.GetType().ToString();
                string errorMessage = errorType + ": " + ex.Message;
                throw new Exception(errorMessage, ex.InnerException);
            }
        }

        public async Task PostRequest(string url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, url));

                if (VariablesGlobales.Token != "")
                {
                    DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", VariablesGlobales.Token);
                }
                var content = new StringContent(String.Empty, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                string errorType = ex.GetType().ToString();
                string errorMessage = errorType + ": " + ex.Message;
                throw new Exception(errorMessage, ex.InnerException);
            }
        }

        public async Task<T2> PostResponse<T, T2>(T item, string url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, url));
                if (VariablesGlobales.Token != "")
                {
                    DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", VariablesGlobales.Token);
                }

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<T2>(responseContent);

                    return Item;
                }

                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T2> PutResponse<T, T2>(T item, string url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, url));
                if (VariablesGlobales.Token != "")
                {
                    DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", VariablesGlobales.Token);
                }

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<T2>(responseContent);

                    return Item;
                }

                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<List<T2>> PostListResponse<T, T2>(T item, string url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, url));
                //if (VariablesGlobales.Token != "")
                //{
                //    DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", VariablesGlobales.Token);
                //}
                DefaultRequestHeaders.ExpectContinue = false;
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await PostAsync(uri, content);

                //var response = await GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var Items = JsonConvert.DeserializeObject<List<T2>>(responseContent);
                    return Items;
                }
                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T2> PostResponseComanda<T, T2>(T item, string url)
        {
            try
            {
                var uri = new Uri(string.Format(url));

                var json = item.ToString();

                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("comanda", json));
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new FormUrlEncodedContent(nvc) };
                var response = await client.SendAsync(req);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<T2>(responseContent);

                    return Item;
                }

                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T2> PostResponseLogin<T, T2>(T item, string url)
        {
            try
            {
                var uri = new Uri(string.Format(Constantes.BaseUrlApi, url));
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var Item = JsonConvert.DeserializeObject<T2>(responseContent);

                    return Item;
                }

                throw new Exception(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CheckInternetConnection()
        {
            string CheckUrl = "http://google.com";

            try
            {

                var uri = new Uri(CheckUrl);
                var response = await GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return true;
                }
                throw new Exception(response.ReasonPhrase);

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());

                return true;

            }
            catch (WebException ex)
            {

                // Console.WriteLine (".....no connection..." + ex.ToString ());

                return false;
            }
        }
    }
}
