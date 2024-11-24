using JWTWPF_Client.Credentials;
using Newtonsoft.Json;
using RegistrationsLiberary.AuthModels;
using RegistrationsLiberary.Registerations;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JWTWPF_Client.HelperManagers
{
    internal static class AuthManager
    {
        public static async Task<Auth> RegisterAsync(RegisterModel registerModel)
        {
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("https://localhost:7124/"),
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(registerModel),
                Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync("api/AthenticationTest/Register", stringContent);
            res.EnsureSuccessStatusCode();
            var result = await res.Content.ReadAsStringAsync();
            var auth = JsonConvert.DeserializeObject<Auth>(result);
            if (res.IsSuccessStatusCode)
            {
                CredentialManager.Token = auth.Token;
                CredentialManager.ExpiredDateTime = auth.ExpiredOn;
                CredentialManager.EmailUserLogined = auth.Email;
            }

            return auth;
        }
        public static async Task<Auth> LoginAsync(LoginModel loginModel)
        {
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("https://localhost:7124/"),
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(loginModel),
                Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync("api/AthenticationTest/Login", stringContent);
            res.EnsureSuccessStatusCode();
            var result = await res.Content.ReadAsStringAsync();
            var auth = JsonConvert.DeserializeObject<Auth>(result);
            if (res.IsSuccessStatusCode)
            {
                CredentialManager.Token = auth.Token;
                CredentialManager.ExpiredDateTime = auth.ExpiredOn;
                CredentialManager.EmailUserLogined = auth.Email;
            }

            return auth;
        }
        public static async Task<string> ForgetPasswordAsync(ForgotPasswordModel forgotPasswordModel)
        {
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("https://localhost:7124/"),
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(forgotPasswordModel),
                Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync("api/AthenticationTest/ForgetPassword", stringContent);
            res.EnsureSuccessStatusCode();
            var result = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(result);
        }
        public static async Task<string> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("https://localhost:7124/"),
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(resetPasswordModel),
                Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync("api/AthenticationTest/ResetPassword", stringContent);
            res.EnsureSuccessStatusCode();
            var result = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<string>(result);
        }
        public static async Task<AddRoolModel> AddRoleToUserAsync(AddRoolModel addRoolModel)
        {
            try
            {
                HttpClient _httpClient = new HttpClient()
                {
                    BaseAddress = new System.Uri("https://localhost:7124/"),
                };
                var token = await CredentialManager.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var stringContent = new StringContent(JsonConvert.SerializeObject(addRoolModel),
                    Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync("api/AthenticationTest/AddRoleToUser", stringContent);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    var result = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AddRoolModel>(result);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }
        public static async Task<List<string>> GetNamesAsync()
        {
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("https://localhost:7124/"),
            };
            var token = await CredentialManager.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new
         System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var res = await _httpClient.GetAsync("api/AthenticationTest/GetNames");
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(result);
            }
            return null;
        }

    }
}
