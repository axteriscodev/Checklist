using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Services;

namespace ServerHost.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class QuestionController : DefaultController
    {


        [LogAction]
        [HttpPost]
        public AXT_WebResponse Prova() 
        { 
         return new AXT_WebResponse();
        }

    }
}
