namespace CoreApiPOC.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Models;
    using System.Diagnostics;

    public class ServiceHandler
    {
        static HttpClient client = new HttpClient();

        public ServiceHandler()
        {

        }

        public static async Task<T> GetDataAsync<T>(string id)
        {
            client.DefaultRequestHeaders.ExpectContinue = false;
            T returnResult = default(T);
            
            client.BaseAddress = new Uri("http://103.15.176.18:8080/users/");

            var response = await client.GetAsync(id);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                returnResult = JsonConvert.DeserializeObject<T>(content);
            }
            return returnResult;
        }

        public static async Task<T> PostDataAsync<T, Tr>(Tr content)
        {
            client.DefaultRequestHeaders.ExpectContinue = false;
            T returnResult = default(T);
            client.BaseAddress = new Uri("http://103.15.176.18:8080/users");
            try
            {
                string jsonString = string.Empty;
                if (content != null) { jsonString = JsonConvert.SerializeObject(content); }
                var httpcontent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(string.Empty, httpcontent);
                returnResult = JsonConvert.DeserializeObject<T>(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceHandler] Error PostDataAsync: {ex.InnerException.ToString()}");
            }
            return returnResult;
        }

        public static async Task<T> AuthAsync<T, Tr>(Tr content)
        {
            client.DefaultRequestHeaders.ExpectContinue = false;
            T returnResult = default(T);

            client.BaseAddress = new Uri("http://103.15.176.18:8080/users/authenticate/");

            try
            {
                string jsonString = string.Empty;
                if (content != null)
                {
                    jsonString = JsonConvert.SerializeObject(content);
                }

                var httpcontent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(string.Empty, httpcontent);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    returnResult = JsonConvert.DeserializeObject<T>(data);
                }
            }
            catch (Exception ex)
            {
                string e = ex.InnerException.ToString();
            }
            return returnResult;
        }

        public static async Task<T> PutDataAsync<T, Tr>(int id, Tr content)
        {
            client.DefaultRequestHeaders.ExpectContinue = false;
            T returnResult = default(T);
            client.BaseAddress = new Uri( string.Format("http://103.15.176.18:8080/users/{0}",id));
            try
            {
                string jsonString = string.Empty;
                if (content != null) { jsonString = JsonConvert.SerializeObject(content); }
                var httpcontent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(string.Empty, httpcontent);
                returnResult = JsonConvert.DeserializeObject<T>(response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ServiceHandler] Error PutDataAsync: {ex.InnerException.ToString()}");
            }
            return returnResult;
        }

        public static async void POSTUserRegister(UserDto obj)
        {
            Uri requestUri = new Uri("http://103.15.176.18:8080/users"); 
            
            string json = string.Empty;
            if (obj != null)
            {
                json = JsonConvert.SerializeObject(obj);
            }
            
            var objClint = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            string responJsonText = await respon.Content.ReadAsStringAsync();
        }


    }
}
