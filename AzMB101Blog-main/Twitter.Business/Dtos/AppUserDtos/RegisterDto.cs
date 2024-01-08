using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.TopicDtos;

namespace Twitter.Business.Dtos.AppUserDtos
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(32);
            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .NotNull()
                .LessThan(DateTime.UtcNow.AddYears(-18)).WithMessage("18 yasdan boyuk olmalidir!!");
                RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(64);
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress(); 
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");
        }
    }
}
