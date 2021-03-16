using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Robson_Totvs_Test.Domain.Entities;
using Robson_Totvs_Test.Domain.Interfaces.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Configuration.TokenService
{
    public class TotvsTokenService : ITotvsTokenService
    {
        private UserManager<Account> _userManager;
        private JwtSettings _jwtSettings;

        public TotvsTokenService(
            UserManager<Account> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> GenerateTokenAsync(string username)
        {
            if (username == null) throw new ArgumentNullException("Username can not be null");

            var myUser = await _userManager.FindByNameAsync(username);

            var tokenHandler = new JwtSecurityTokenHandler();
            var mySecret = this._jwtSettings.Secret;
            var mySecretInBytes = Encoding.UTF8.GetBytes(mySecret);

            var myDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(mySecretInBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(myDescriptor));
        }
    }
}
