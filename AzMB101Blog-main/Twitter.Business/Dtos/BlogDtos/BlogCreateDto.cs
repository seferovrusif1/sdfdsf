using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.Dtos.TopicDtos;

namespace Twitter.Business.Dtos.BlogDtos
{
    public class BlogCreateDto
    {
        public string Description { get; set; }

    }
    public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
    {
        public BlogCreateDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(1024);

        }
    }
}
