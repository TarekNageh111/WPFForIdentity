using Newtonsoft.Json;
using RegistrationsLiberary.AuthModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JWTWPF_Client.Credentials
{
    public static class CredentialManager
    {
        public static string Token { get; set; }
        public static DateTime ExpiredDateTime { get; set; }
        public static string EmailUserLogined { get; set; }
        public static async Task<string> GetToken()
        {
            if (ExpiredDateTime.Hour > DateTime.UtcNow.Hour)
                return Token;
            else if (ExpiredDateTime.Hour <= DateTime.UtcNow.Hour)
            {
                var getRefreshToken = await GetRefreshToken();
                if (getRefreshToken != null)
                {
                    Token = getRefreshToken.Token;
                    ExpiredDateTime = getRefreshToken.ExpiredOn;
                    EmailUserLogined = getRefreshToken.Email;
                }
            }
            return Token;
        }
        private static async Task<Auth> GetRefreshToken()
        {
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new System.Uri("https://localhost:7124/"),
            };
            var res = await _httpClient.GetAsync($"api/AthenticationTest/GetRefreshToken?email={EmailUserLogined}");
            res.EnsureSuccessStatusCode();

            var result = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Auth>(result);
        }
    }
}
//private static readonly string AppName = "myjwtclient";
//private static readonly string TokenFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{AppName}_token.dat");

//public static void SaveToken(string token)
//{
//    // Convert the token to bytes
//    var tokenBytes = Encoding.UTF8.GetBytes(token);

//    // Encrypt the token
//    var encryptedToken = ProtectedData.Protect(tokenBytes, null, DataProtectionScope.CurrentUser);

//    // Save the encrypted token to a file
//    File.WriteAllBytes(TokenFilePath, encryptedToken);
//}

//public static string GetToken()
//{
//    if (!File.Exists(TokenFilePath))
//        throw new FileNotFoundException("Token file not found.");

//    // Read the encrypted token from the file
//    var encryptedToken = File.ReadAllBytes(TokenFilePath);

//    // Decrypt the token
//    var decryptedToken = ProtectedData.Unprotect(encryptedToken, null, DataProtectionScope.CurrentUser);

//    // Convert the decrypted token back to a string
//    return Encoding.UTF8.GetString(decryptedToken);
//}

//public static void ClearToken()
//{
//    if (File.Exists(TokenFilePath))
//        File.Delete(TokenFilePath);
//}