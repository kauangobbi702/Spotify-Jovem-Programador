using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpotifyRecom.Model;

namespace SpotifyRecom.Service;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var chaveSecreta = GetSigningKey(_configuration);
        var expiration = GetExpiration(_configuration);

        var credenciais = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new(ClaimTypes.Email, usuario.Email)
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credenciais,
            Expires = expiration,
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static SymmetricSecurityKey GetSigningKey(IConfiguration configuration)
    {
        var secretKey = configuration["JWTSettings:SecretKey"];
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException(
                "Configure JWTSettings:SecretKey using a secure configuration provider.");
        }

        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        if (keyBytes.Length < 32)
        {
            throw new InvalidOperationException(
                "JWTSettings:SecretKey must contain at least 32 UTF-8 bytes.");
        }

        return new SymmetricSecurityKey(keyBytes);
    }

    private static DateTime GetExpiration(IConfiguration configuration)
    {
        var expirationInMinutes = configuration["JWTSettings:ExpirationInMinutes"];
        if (double.TryParse(
                expirationInMinutes,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var minutes) &&
            double.IsFinite(minutes) &&
            minutes > 0)
        {
            return DateTime.UtcNow.AddMinutes(minutes);
        }

        var expirationInHours = configuration["JWTSettings:ExpirationInHours"];
        if (double.TryParse(
                expirationInHours,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var hours) &&
            double.IsFinite(hours) &&
            hours > 0)
        {
            return DateTime.UtcNow.AddHours(hours);
        }

        throw new InvalidOperationException(
            "Configure JWTSettings:ExpirationInMinutes or JWTSettings:ExpirationInHours with a positive number.");
    }
}