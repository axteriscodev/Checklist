using AXT_WebComunication.WebResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerHost.Model;
using ServerHost.Services;
using Shared.ApiRouting;
using Shared.Login;
using Shared.Organizations;
using System.Collections.Generic;
using TDatabase.Queries;

namespace ServerHost.Controllers
{
    [ApiController]
    public class UserController(AccessService accessService) : DefaultController
    {

        private readonly AccessService _accessService = accessService;

        #region Login

        [LogAction]
        [Route(ApiRouting.Login)]
        [HttpPost]
        public AXT_WebResponse Login(UserLoginRequest rq)
        {
            var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
                var user = UserDbHelper.SelectUserFromEmail(db, rq.Username);
                if(user is not null)
                {
                    if(CryptUtilities.CryptPassword(rq.Password, user.Salt).Equals(user.Password))
                    {
                        var userModel = UserDbHelper.CreateUserModel(db, user);
                        var jwt = JWTManager.GeneraJSONWebToken(userModel);
                        response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), jwt);
                    }
                    else
                    {
                        response.AddResponse(StatusResponse.GetStatus(Status.LOGIN_ERROR), "");
                    }
                }
                else
                {
                    response.AddResponse(StatusResponse.GetStatus(Status.LOGIN_ERROR), "");
                }
            }
            catch (Exception ex)
            {
                response = ExceptionWebResponse(ex, "");
            }
            StopTime(stopwatch);
            return response;
        }

        #endregion

        #region Users

        [LogAction]
        [Route(ApiRouting.UserList)]
        [HttpPost]
        public AXT_WebResponse UserList()
        {
            var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
                var idOrganizzation = GetUserOrganization();
                var list = UserDbHelper.Select(db, idOrganizzation);
                response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), list);

            }
            catch (Exception ex)
            {
                response = ExceptionWebResponse(ex, "");
            }
            StopTime(stopwatch);
            return response;
        }

        [LogAction]
        [Route(ApiRouting.SaveUser)]
        [HttpPost]
        public async Task<AXT_WebResponse> SaveUser(UserModel newUser)
        {
            var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
                var password = CryptUtilities.CreateNewPassword(newUser.Password);
                var list = await UserDbHelper.Insert(db, newUser, password);
                response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), list);

            }
            catch (Exception ex)
            {
                response = ExceptionWebResponse(ex, "");
            }
            StopTime(stopwatch);
            return response;
        }

        [LogAction]
        [Route(ApiRouting.UpdateUsers)]
        [HttpPost]
        public async Task<AXT_WebResponse> UpdateUsers(List<UserModel> users)
        {
            var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
                var list = await UserDbHelper.Update(db, users);
                var jwt = JWTManager.GeneraJSONWebToken(users.First());
                response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), jwt);

            }
            catch (Exception ex)
            {
                response = ExceptionWebResponse(ex, "");
            }
            StopTime(stopwatch);
            return response;
        }

        [LogAction]
        [Authorize]
        [Route(ApiRouting.ChangePassword)]
        [HttpPost]
        public async Task<AXT_WebResponse> ChangePassword(ChangePasswordRequest rq)
        {
             AXT_WebResponse response = new();
            ConfigureLog(rq, 3);
            var stopwatch = StartTime();
            var db = GetDbConnection();
            try
            {
                var access = new UserModel() { Email = rq.Email, Password = rq.OldPassword };
                var success = await _accessService.ChangePassword(access,  rq.NewPassword);
                response = success ? new(StatusResponse.GetStatus(Status.SUCCESS), "")
                                   : new(StatusResponse.GetStatus(Status.NO_UPDATE), "");
            }
            catch (Exception e)
            {
                response = ExceptionWebResponse(e, "");
            }
            StopTime(stopwatch);
             return response;
        }

        [LogAction]
        [Route(ApiRouting.SendToken)]
        [HttpPost]
        public async Task<AXT_WebResponse> SendToken(EmailRequest rq)
        {
            AXT_WebResponse response;
            ConfigureLog(rq, 3);
            var stopwatch = StartTime();
            try
            {
                var success = await _accessService.SendResetToken(rq.Email);
                response = success ? new(StatusResponse.GetStatus(Status.SUCCESS), "")
                                   : new(StatusResponse.GetStatus(Status.INSERT_ERROR), "");
            }
            catch (Exception e)
            {
                response = ExceptionWebResponse(e, "");
            }
            StopTime(stopwatch);
            return response;
        }

        [LogAction]
        [Route(ApiRouting.VerifyToken)]
        [HttpPost]
        public AXT_WebResponse VerifyToken(VerifyResetTokenRequest rq)
        {
            AXT_WebResponse response;
            ConfigureLog(rq, 3);
            var stopwatch = StartTime();
            try
            {
                var success = _accessService.VerifyResetToken(rq.Email, rq.ResetToken);
                response = success ? new(StatusResponse.GetStatus(Status.SUCCESS), "")
                                   : new(StatusResponse.GetStatus(Status.INSERT_ERROR), "");
            }
            catch (Exception e)
            {
                response = ExceptionWebResponse(e, "");
            }
            StopTime(stopwatch);
            return response;
        }

        [LogAction]
        [Route(ApiRouting.ChangePasswordWithToken)]
        [HttpPost]
        public async Task<AXT_WebResponse> ChangePasswordWithToken(ChangePasswordRequest rq)
        {
            AXT_WebResponse response;
            ConfigureLog(rq, 3);
            var stopwatch = StartTime();
            try
            {
                var success = await _accessService.ChangePasswordWithToken(rq.Email, rq.ResetToken, rq.NewPassword);
                response = success ? new(StatusResponse.GetStatus(Status.SUCCESS), "")
                                   : new(StatusResponse.GetStatus(Status.INSERT_ERROR), "");
            }
            catch (Exception e)
            {
                response = ExceptionWebResponse(e, "");
            }
            StopTime(stopwatch);
            return response;
        }

        [LogAction]
        [Route(ApiRouting.HideUsers)]
        [HttpPost]
        public async Task<AXT_WebResponse> HideCategories(List<UserModel> users)
        {
            var response = new AXT_WebResponse();
            var stopwatch = StartTime();
            ConfigureLog("", 0);

            try
            {
                var db = GetDbConnection();
                var list = await UserDbHelper.Hide(db, users);
                response.AddResponse(StatusResponse.GetStatus(Status.SUCCESS), list);

            }
            catch (Exception ex)
            {
                response = ExceptionWebResponse(ex, "");
            }
            StopTime(stopwatch);
            return response;
        }

        #endregion

    }
}
