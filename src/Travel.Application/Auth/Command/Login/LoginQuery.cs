using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Application.Auth.Command.Login
{
   public  class LoginQuery:IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
