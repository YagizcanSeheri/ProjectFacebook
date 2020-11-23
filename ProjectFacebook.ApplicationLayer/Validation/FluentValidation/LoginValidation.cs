using FluentValidation;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Validation.FluentValidation
{
    public class LoginValidation : AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a username");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter a password..!");
        }
    }
}
