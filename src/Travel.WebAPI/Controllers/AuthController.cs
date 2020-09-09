using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Auth.Command.Login;

namespace Travel.WebAPI.Controllers
{
    public class AuthController :BaseController
    {

          [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginQuery query)
        {
            return await Mediator.Send(query);

        }
    }
}