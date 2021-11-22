using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Riddha.CourierMS.Common.Services
{
    public class WebrequestService
    {
        WebClient wc = null;
        public WebrequestService()
        {
            wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers.Add("Content-Type", "application/json; charset=utf-8");
            wc.Headers.Add("Accept", "application/json");
        }

        public async Task<T> Get<T>(string url)
        {
            try
            {
                var handler = new HttpClientHandler()
                {
                    AllowAutoRedirect = true,
                    MaxAutomaticRedirections = 100,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                var client = new HttpClient(handler);

                client.MaxResponseContentBufferSize = 256000;
                if (!string.IsNullOrEmpty(InitializeDB.UserInfo.ServerIp))
                {
                    client.BaseAddress = new Uri("http://" + InitializeDB.UserInfo.ServerIp);
                }
                else
                {
                    client.BaseAddress = new Uri(CONSTANTS.BaseUrl);
                }

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
                //client.DefaultRequestHeaders.Add("Token", InitializeDB.UserInfo.Token);
                try
                {
                    var response = client.GetAsync(url).GetAwaiter().GetResult();

                    T data = await DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    return data;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> Post<T>(string url, string jsonSerializeObject)
        {

            try
            {
                var handler = new HttpClientHandler()
                {
                    AllowAutoRedirect = true,
                    MaxAutomaticRedirections = 100,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate

                };

                var client = new HttpClient(handler);

                client.MaxResponseContentBufferSize = 256000000;
                if (!string.IsNullOrEmpty(InitializeDB.UserInfo.ServerIp))
                {
                    client.BaseAddress = new Uri("http://" + InitializeDB.UserInfo.ServerIp);
                }
                else
                {
                    client.BaseAddress = new Uri(CONSTANTS.BaseUrl);
                }

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("gzip"));
                client.DefaultRequestHeaders.AcceptEncoding.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue("deflate"));
                //client.DefaultRequestHeaders.Add("Token", InitializeDB.UserInfo.Token);
                var content = new StringContent(jsonSerializeObject.ToString(), Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var str = response.Content.ReadAsStringAsync().Result;
                        //var a = (new JavaScriptSerializer()).Deserialize<T>(response.Content.ReadAsStringAsync().Result);
                        //var str =  response.Content.ReadAsStringAsync<T>().Result;

                        T data = await DeserializeObject<T>(str);
                        //response.Dispose();
                        return data;
                    }
                    else
                    {
                        return await DeserializeObject<T>("");
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public string SerializeObject<T>(T obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            return content;
        }


        public async Task<T> DeserializeObject<T>(string obj)
        {
            //var items = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj));
            //return items;
            var items = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);
            return items;


        }
    }
}
