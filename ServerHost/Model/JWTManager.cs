using Microsoft.IdentityModel.Tokens;
using ServerHost.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Shared.Organizations;
using Shared.Login;

namespace ServerHost.Model;

/// <summary>
/// Classe utilizzata per la gestione del JWT per autenticazione ed autorizzazione
/// </summary>
public class JWTManager
{
    /// <summary>
    /// Metodo che generera un JWT utilizzando le informazioni di configurazione presenti nel file
    /// appsettings.json e dall'oggetto Shared_Utente con le informazioni dell'utente che si è autenticato
    /// </summary>
    /// <param name="user">oggetto con le info dell'utente</param>
    /// <param name="configuration">servizio per accedere alla configurazione appsettings.json</param>
    /// <returns></returns>
    public static string GeneraJSONWebToken(UserModel user)
    {
        var data = DateTime.UtcNow;
        var expires = DateTime.UtcNow.AddDays(1);
        SymmetricSecurityKey symmetricSecurityKey = new(
        Encoding.UTF8.GetBytes(ConfigurationService.GetConfiguration("Jwt:SecurityKey")!));
        SigningCredentials credentials = new(
            symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
               new(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new(ClaimTypes.Name, user.Name),
               new(ClaimTypes.Role, user.Role.Name),
               new(UserClaims.User,JsonSerializer.Serialize(user)),
        };

        JwtSecurityToken jwtSecurityToken = new(ConfigurationService.GetConfiguration("Jwt:Issuer"), ConfigurationService.GetConfiguration("Jwt:Audience"), claims, data, expires, credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
