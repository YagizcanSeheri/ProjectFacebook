using FluentValidation;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Validation.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Enter a email address..!")
                .EmailAddress()
                .WithMessage("Please enter valid email address..!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter a password..!");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Password don't match..!");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("User name can't be emty..!")
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Lenght must be between 3 and 50 characters..!");
        }
    }
}
