using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using fruitful.dal.Collections;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Fruitful.BLL.Services;

public class JWTService
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Account account, IEnumerable<Claim> additionalClaims = null)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.Username),
            new Claim(ClaimTypes.Role, account.Role)
        };
        if (additionalClaims != null)
        {
            claims.AddRange(additionalClaims);
        }

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWTConfig:SecretKey").Value));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public string GetRoleFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var role = jwt.Claims.First(claim => claim.Type == ClaimTypes.Role);

        return role.Value;
    }

    public string GetUsernameFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);
        var username = jwt.Claims.First(claim => claim.Type == ClaimTypes.Name);

        return username.Value;
    }

    public bool IsExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadToken(token);
        if (token == null) return false;
        return DateTime.UtcNow > jwt.ValidTo;
    }
}