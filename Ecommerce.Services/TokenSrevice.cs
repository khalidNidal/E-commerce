﻿using Ecommerce.Core.Entities;
using Ecommerce.Core.IRepositories.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class TokenSrevice : ITokenServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<LocalUser> userManager;
        private readonly string secretKey;

        public TokenSrevice(IConfiguration configuration , UserManager<LocalUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            secretKey = configuration.GetSection("ApiSetting")["SecretKey"];

        }


        public async Task<string> CreateTokenAsync(LocalUser localUser)
        {
            var Name = localUser.FirstName + " " + localUser.LastName;
            var key = Encoding.ASCII.GetBytes(secretKey);
            var roles = await userManager.GetRolesAsync(localUser);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , Name)

            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new  SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature),

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptior);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
