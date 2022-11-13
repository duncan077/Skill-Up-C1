using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AlkemyWallet.Core.Helper
{
    public class JWTAuthManager : IJWTAuthManager
    {
        private IConfiguration _configuration;
        public JWTAuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public byte[] GetSalt()
        {
            return Encoding.UTF8.GetBytes(_configuration.GetSection("secret").ToString());
        }
        public string CreatePasswordHash(string password)
        {

            using (var hmac = new HMACSHA512(GetSalt()))
            {
                byte[] passwordSalt = hmac.Key;
                return Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));

            }
        }

        public string CreateToken(string userName, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings").Value));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public bool VerifyPasswordHash(string password, string passwordHash)
        {

            using (var hmac = new HMACSHA512(GetSalt()))
            {
                var computeddHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeddHash.SequenceEqual(Convert.FromBase64String(passwordHash));
            }
        }
    }
}

