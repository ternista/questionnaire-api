using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Questionnaires.Api.AuthenticationMock
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Dictionary<string, string> _userIdMap = new Dictionary<string, string>
        {
            { "bob@gmail.com", "1" },
            { "roos@gmail.com", "2" },
            { "sarah@gmail.com", "3" }
        };
        
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponse Authenticate(GetTokenRequest request)
        {
            var email = request.Email?.ToLower() ?? "sahra@gmail.com";

            var userId = _userIdMap.ContainsKey(email) ? _userIdMap[email] : "3";
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Test user"),
                new Claim(ClaimTypes.Email, email),
                new Claim(ApplicationClaimTypes.UserId, userId),
                new Claim(ApplicationClaimTypes.DepartmentId, "232"),
                new Claim(ApplicationClaimTypes.CompanyId, "424")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
                _configuration["JWT:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = token.ValidTo
            };
        }
    }
}