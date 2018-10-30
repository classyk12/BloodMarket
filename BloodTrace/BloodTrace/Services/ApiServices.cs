using BloodTrace.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using BloodTrace.Helpers;
using System.Collections.ObjectModel;

namespace BloodTrace.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterUser(string email, string password, string confirmpassword)
        {
            var registermodel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmpassword
            };
            var httpclient = new HttpClient();
            var json = JsonConvert.SerializeObject(registermodel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await httpclient.PostAsync("http://bloodmarket.azurewebsites.net/api/Account/Register", content);
            return reponse.IsSuccessStatusCode;
        }

        //public void LoginUser (string email, string password)
        //{
        //    new List<KeyValuePair<string, string>>()
        //    {
        //        new KeyValuePair<string, string>("username",email),
        //         new KeyValuePair<string, string>("username",password),
        //          new KeyValuePair<string, string>("grant_type",password),
        //    };


        public async Task<bool> LoginUser(string email, string password)
        {
            //because during using postman to try get a token to login, we had to supply 
            //an email,password and a grant_type to be able to get a token i.e Login
            //its a combination of a key and a value, thats is why a list of keyvalue pair of type string is used
            var keyvaluepair = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username",email),
                new KeyValuePair<string, string>("password",password),
                new KeyValuePair<string, string>("grant_type","password")
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "http://bloodmarket.azurewebsites.net/Token");
            request.Content = new FormUrlEncodedContent(keyvaluepair);
            var httpclient = new HttpClient();
            var response = await httpclient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            JObject jobject =  JsonConvert.DeserializeObject<dynamic>(content);
            var accessToken =  jobject.Value<string>("access_token");
            Settings.AccessToken = accessToken;
            Settings.UserName = email;
            Settings.Password = password;
            return response.IsSuccessStatusCode;
        }
        public async Task<List<BloodUser>> FindBlood (string bloodtype, string country)
        {
            try
            {
                var httpclient = new HttpClient();
                httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
                var BloodApiUrl = "http://bloodmarket.azurewebsites.net/api/BloodModels";
                var json = await httpclient.GetStringAsync($"{BloodApiUrl}?bloodgroup={bloodtype}&country={country}");
                return JsonConvert.DeserializeObject<List<BloodUser>>(json);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
            
        }
        public async Task<List<BloodUser>> LatestBloodUser()
        {
            var httpclient = new HttpClient();
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var BloodApiUrl = "http://bloodmarket.azurewebsites.net/api/BloodModels";
            var json = await httpclient.GetStringAsync(BloodApiUrl);
            return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        }
            
        public async Task<bool> RegisterDonor(BloodUser blooduser)
        {
            var json = JsonConvert.SerializeObject(blooduser);
            var httpclient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
            var BloodApiUrl = "http://bloodmarket.azurewebsites.net/api/BloodModels";
           var response=  await httpclient.PostAsync(BloodApiUrl,content);
            return response.IsSuccessStatusCode;
           
        }
        //public async Task<List<BloodUser>> GetDonors(string email)
        //{
        //    var httpclient = new HttpClient();
        //    httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Settings.AccessToken);
        //    var BloodApiUri ="http://bloodmarket.azurewebsites.net/api/BloodModels";
        //    var json = await httpclient.GetStringAsync($"{BloodApiUri}?email={email}");
        //    return JsonConvert.DeserializeObject<List<BloodUser>>(json);
        //}
    }
}
