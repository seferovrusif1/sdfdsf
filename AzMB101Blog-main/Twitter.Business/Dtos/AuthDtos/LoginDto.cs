using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.BlogDtos;

namespace Twitter.Business.Dtos.AuthDtos
{
    public class LoginDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty()
                .NotNull()
                .MaximumLength(64)
                .MinimumLength(3);
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(6);
        }
    }
}
