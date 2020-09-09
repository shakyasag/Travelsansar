using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Auth.Command.Login;
using Travel.Application.Interface;
using Travel.Entities;

namespace Travel.Infrastructure.Common
{
     public class JwtGenerator:IJwtGenerator
    {
        public readonly SignInManager<User> _signinmanager;
        public readonly UserManager<User> _usermanager;
        public readonly IConfiguration _config;

        public JwtGenerator(SignInManager<User> signinmanager, UserManager<User> usermanager, IConfiguration config)
        {
            _signinmanager = signinmanager;
            _usermanager = usermanager;
            _config = config;
        }
        public async Task<string> GetJwtToken(User user)
        {
            var claim = new List<Claim>
            {
          new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
           new Claim(ClaimTypes.Name,user.UserName.ToString()),

            };
            var role = await _usermanager.GetRolesAsync(user);
            foreach (var roles in role)
            {
                claim.Add(new Claim(ClaimTypes.Role, roles));
            }
            var tokendesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)), SecurityAlgorithms.HmacSha256Signature)
             };    
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendesc);
            return tokenhandler.WriteToken(token);


        }

      
    }
}
