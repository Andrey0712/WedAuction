﻿using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAuction.Abstract;

namespace WebAuction.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        public JwtTokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _config = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            List<Claim> claims = new List<Claim>()
            {
                new Claim("name", user.Name),
                new Claim("email", user.Email),
            new Claim("avatar", user.Avatar ?? string.Empty),

        };

            if (roles.Any())
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim("roles", role));
                }
            }
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("JwtKey")));
            var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signinCredentials,
                expires: DateTime.Now.AddDays(10),
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);


           
        }

        
    }
}
