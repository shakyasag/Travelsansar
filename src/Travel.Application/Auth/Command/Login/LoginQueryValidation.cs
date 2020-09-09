using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Application.Auth.Command.Login
{
     public class LoginQueryValidation:AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
