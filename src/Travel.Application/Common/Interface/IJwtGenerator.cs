using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Auth.Command.Login;
using Travel.Entities;

namespace Travel.Application.Interface
{
    public interface IJwtGenerator
    {
        Task<string> GetJwtToken(User user);
    }
}
