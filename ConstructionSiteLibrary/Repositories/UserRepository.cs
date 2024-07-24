using AXT_WebComunication.WebRequest;
using AXT_WebComunication.WebResponse;
using ConstructionSiteLibrary.Managers;
using Shared.ApiRouting;
using Shared.Documents;
using Shared.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConstructionSiteLibrary.Repositories
{
    public class UserRepository(HttpManager httpManager)
    {

        private readonly HttpManager _httpManager = httpManager;


        public async Task<AXT_WebResponse> Login(UserLoginRequest rq)
        {
            var response = await _httpManager.SendHttpRequest(ApiRouting.Login, rq);
            return response;
        }
    }
}
