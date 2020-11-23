using FluentValidation;
using ProjectFacebook.ApplicationLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Validation.FluentValidation
{
    public class PostValidation : AbstractValidator<SendPostDTO>
    {
        public PostValidation()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("This field can't be emty..!")
                .MaximumLength(280)
                .WithMessage("Less than 280 characters..!");
        }
    }
}
