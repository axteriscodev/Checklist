using System;
using ConstructionSiteLibrary.Model;
using ConstructionSiteLibrary.Repositories;
using ServerHost.Model;
using ServerHost.Services.Interfaces;
using Shared.Organizations;
using TDatabase.Database;
using TDatabase.Queries;
using TDatabase.Utilities;

namespace ServerHost.Services;

public class AccessService(IMailService mailService)
{
    private readonly IMailService _mailService = mailService;

    public async Task<bool> ChangePassword(UserModel access, string newPassword)
        {
            var result = false;
            var db = GetDbConnection();
            var user = ValidateAccessData(access);
            if(user is not null)
            {
                var salt = CryptUtilities.CreateRandomString();
                var password = CryptUtilities.CryptPassword(access.Password, salt);
                result = await UserDbHelper.ChangePassword(db, user.Email, password, salt);
            }
            return result;
        }

        // public async Task<bool> RegisterNewUser(UserAccessInfo access)
        // {
        //     var salt = CryptUtilities.CreateRandomString();
        //     var password = CryptUtilities.CryptPassword(access.Password, salt);
        //     var user = new UserModel
        //     {
        //         Id = 0,
        //         Name = access.Name,
        //         Surname = access.Surname,
        //         Uid = access.Uid,
        //         Password = password,
        //         Salt = salt
        //     };
        //     var result = await _accessRepository.InsertUser(user);
        //     return result;
        // }


        // public bool CheckAccessToken(string token, bool isAdmin = false)
        // {
        //     return isAdmin ? _authTokenService.ValidateJWTToken(token) : _accessRepository.FindAccessToken(token);
        // }

        
       
        public async Task<bool> SendResetToken(string email)
        {
            var result = false;
            var db = GetDbConnection();
            var token = CryptUtilities.CreateRandomString(8,false);
            var insert = await UserDbHelper.InsertResetToken(db, email, token);
            if(insert)
            {
                var mail = new Mail()
                {
                    //se è stato specificato un destinatario, la mail viene inviata all'indirizzo
                    //inserito altrimenti viene inviata all'indirizzo di default
                    Receiver = email,
                };
                mail.CreateTokenEmail(token);
                result = await _mailService.SendMail(mail);
            }
            return result;
        }
        public bool VerifyResetToken(string email, string token)
        {
            var db = GetDbConnection();
            var user = UserDbHelper.SelectUserFromEmail(db, email);
            var result = user is not null 
                  && user.ResetToken.Equals(token) 
                  && user.ResetTokenExpirationDate > DateTime.Now;
            return result;
        }
        public async Task<bool> ChangePasswordWithToken(string email, string token, string newPassword)
        {
            var result = false;
            var db = GetDbConnection();
            var user = UserDbHelper.SelectUserFromEmail(db, email);
            if (user is not null && user.ResetToken.Equals(token))
            {
                var salt = CryptUtilities.CreateRandomString();
                var password = CryptUtilities.CryptPassword(newPassword, salt);
                result = await UserDbHelper.ChangePassword(db, email, password, salt);
            }
            return result;
        }

         #region Metodi privati

        private UserModel? ValidateAccessData(UserModel access)
        {
            UserModel? result = null;
            var db = GetDbConnection();
            var user = UserDbHelper.SelectUserFromEmail(db, access.Email);
            if (user is not null)
            {
                //verifico la password
                if (user.Password.Equals(CryptUtilities.CryptPassword(access.Password, user.Salt)))
                {
                    result = UserDbHelper.CreateUserModel(db, user);
                }
            }
            return result;
        }

        private DbCsclDamicoV2Context GetDbConnection()
        {
            var connectionString = ConfigurationService.GetConnection() ?? "";
            return DatabaseContextFactory.Create(connectionString);
        }

        #endregion
}
