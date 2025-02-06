using AXT_WebComunication.WebRequest;
using AXT_WebComunication.WebResponse;
using ConstructionSiteLibrary.Managers;
using ConstructionSiteLibrary.Services;
using Shared.ApiRouting;
using Shared.Documents;
using Shared.Login;
using Shared.Organizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace ConstructionSiteLibrary.Repositories
{
    public class UserRepository(HttpManager httpManager, AppAuthenticationStateProvider appAuth)
    {

        private readonly HttpManager _httpManager = httpManager;

        private readonly AppAuthenticationStateProvider _appAuth = appAuth;


        public async Task<AXT_WebResponse> Login(UserLoginRequest rq)
        {
            var response = await _httpManager.SendHttpRequest(ApiRouting.Login, rq);
            return response;
        }

        public async Task<AXT_WebResponse> UpdateUsers(List<UserModel> rq)
        {
            var response = await _httpManager.SendHttpRequest(ApiRouting.UpdateUsers, rq);

            if(response.Code.Equals("0"))
            {
                var jwt = response.Content.ToString();
                if(!string.IsNullOrEmpty(jwt))
                {
                     await _appAuth.SaveAuthenticationAsync(jwt, true);
                }
               
            }
           
            return response;
        }

        public async Task<bool> ChangePassword(ChangePasswordRequest rq)
        {
            bool result = false;
            var response = await _httpManager.SendHttpRequest(ApiRouting.ChangePassword, rq);
            if (response.Code.Equals(Status.SUCCESS))
            {
                result = true;
            }
            //_notificationService.Notify(response.Code);
            return result;
        }

        public async Task<bool> SendResetToken(string uid)
        {
            bool result = false;
            EmailRequest rq = new() { Email = uid };
            var response = await _httpManager.SendHttpRequest(ApiRouting.SendToken, rq);
            if (response.Code.Equals(Status.SUCCESS))
            {
                result = true;
            }
            //_notificationService.Notify(response.Code);
            return result;
        }

        public async Task<bool> VerifyResetToken(string uid, string token)
        {
            bool result = false;
            VerifyResetTokenRequest rq = new() { Email = uid, ResetToken = token };
            var response = await _httpManager.SendHttpRequest(ApiRouting.VerifyToken, rq);
            if (response.Code.Equals(Status.SUCCESS))
            {
                result = true;
            }
            //_notificationService.Notify(response.Code);
            return result;
        }

        public async Task<bool> ChangePasswordWithToken(string uid, string token, string newPassword)
        {
            bool result = false;
            ChangePasswordRequest rq = new() { Email = uid, ResetToken = token, NewPassword = newPassword };
            var response = await _httpManager.SendHttpRequest(ApiRouting.ChangePasswordWithToken, rq);
            if (response.Code.Equals(Status.SUCCESS))
            {
                result = true;
            }
            //_notificationService.Notify(response.Code);
            return result;
        }

        private class TokenResponse
        {
            [JsonPropertyName("token")]
            public string Token { get; set; } = "";
        }
    }
}
