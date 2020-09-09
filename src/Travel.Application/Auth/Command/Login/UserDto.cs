using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Application.Auth.Command.Login
{
     public class UserDto
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserRole { get; set; }
    }
}
