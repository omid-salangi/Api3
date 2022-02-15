﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Common.SiteSettings;
using Domain.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly SiteSettings _siteSettings;
        public JwtServices(IOptionsSnapshot<SiteSettings> settings)
        {
            _siteSettings = settings.Value;
        }
        public async Task<string> Generate(User user)
        {
            var claims = await _GetClaims(user);
            var securitykey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey); // it must be longer than 16 character
            var encryptkey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.Encryptkey);
            var signincredintial = new SigningCredentials(new SymmetricSecurityKey(securitykey), SecurityAlgorithms.HmacSha256Signature);
            var encryptingcredentails = new EncryptingCredentials(new SymmetricSecurityKey(encryptkey),
                SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = _siteSettings.JwtSettings.Issuer, // sender
                Audience = _siteSettings.JwtSettings.Audience, // receiver 
                IssuedAt = DateTime.Now, // date time of creating token
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinutes), // when we can use token
                Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpirationMinutes), // expire time of token
                SigningCredentials = signincredintial, // needed property for create 
                EncryptingCredentials = encryptingcredentails,
                Subject = new ClaimsIdentity(claims)
            };
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // for name of claims dont change
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;   // if we use this code parameters doesnt change to jwt claims from asp.net claims
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();


            var tokenhandler = new JwtSecurityTokenHandler();
            var securitytoken = tokenhandler.CreateToken(descriptor);
            return tokenhandler.WriteToken(securitytoken);
        }

        public async Task<IEnumerable<Claim>> _GetClaims(User user)
        {
            var list = new List<Claim>()
            {
                new Claim(ClaimTypes.Name , user.USerName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone,"09175312786")
            };
            var roles = new Roles[] { new Roles() { name = "Admin" } };
            foreach (var role in roles)
            {
                list.Add(new Claim(ClaimTypes.Role, role.name));
            }
            return list;
        }
    }
}
