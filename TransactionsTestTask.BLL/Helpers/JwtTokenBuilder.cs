using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransactionsTestTask.BLL.Models;
using TransactionsTestTask.DAL.Entities;

namespace TransactionsTestTask.BLL.Helpers
{
    public class JwtTokenBuilder : IJwtTokenBuilder
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _durationInMinutes;

        public JwtTokenBuilder(IConfiguration configuration)
        {
            _secretKey = configuration["JwtSettings:Key"] ?? throw new ArgumentException("Jwt Key is required");
            _issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentException("Jwt Issuer is required");
            _audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentException("Jwt Audience is required");
            _durationInMinutes = configuration["JwtSettings:DurationInMinutes"] ?? throw new ArgumentException("Jwt DurationInMinutes is required");
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("id", user.Id)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_durationInMinutes)),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
