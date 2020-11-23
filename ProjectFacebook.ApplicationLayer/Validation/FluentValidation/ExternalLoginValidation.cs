using FluentValidation;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Validation.FluentValidation
{
    public class ExternalLoginValidation : AbstractValidator<ExternalLoginDTO>
    {
        public ExternalLoginValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Enter a email address..!")
                .EmailAddress()
                .WithMessage("Please type into valid email address..!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please type into your name")
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Lenght must be between 3 and 50 characters..!");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Please type into your name")
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Lenght must be between 3 and 50 characters..!");
        }
    }
}
