using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class JwtServices : IJwtServices
    {
        public async Task<string> Generate(User user)
        {
            var claims = await _GetClaims(user);
            var securitykey = Encoding.UTF8.GetBytes(">=%2TPd\\3!?~9eed"); // it must be longer than 16 character
            var signincredintial = new SigningCredentials(new SymmetricSecurityKey(securitykey), SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = "Blog", // sender
                Audience = "Blog", // receiver 
                IssuedAt = DateTime.Now, // date time of creating token
                NotBefore = DateTime.Now, // when we can use token
                Expires = DateTime.Now.AddHours(1), // expire time of token
                SigningCredentials = signincredintial, // needed property for create 
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
