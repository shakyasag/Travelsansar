using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Common.Error;
using Travel.Application.Interface;
using Travel.Entities;

namespace Travel.Application.Auth.Command.Login
{
    public class LoginHandlerQuery : IRequestHandler<LoginQuery, UserDto>
    {
        public readonly SignInManager<User> _signinmanager;
        public readonly UserManager<User> _usermanager;
        public readonly IJwtGenerator _jwtGenerator;

         public LoginHandlerQuery(SignInManager<User> signinmanager, UserManager<User> usermanager, IJwtGenerator jwtGenerator)
         {
            _signinmanager = signinmanager;
            _usermanager = usermanager;
            _jwtGenerator = jwtGenerator;
        
          }
        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _usermanager.FindByNameAsync(request.UserName);
             if(user==null)
            {
                throw new RestException (HttpStatusCode.Unauthorized,
                      new { error = "Username Is Invalid" });
            }
            var result = await _signinmanager.CheckPasswordSignInAsync(user, request.Password, false);
             if(result.Succeeded)
              {
                return new UserDto
                {
                    Id = user.Id,
                    Token = await _jwtGenerator.GetJwtToken(user),
                    UserName = user.UserName
                };
             }

            throw new RestException(HttpStatusCode.Unauthorized,
                        new { error = "Password Is Invalid" });
        }
    }
}
