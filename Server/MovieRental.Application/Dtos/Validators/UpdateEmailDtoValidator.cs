using FluentValidation;
using MovieRental.Application.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Validators
{
    public class UpdateEmailDtoValidator : AbstractValidator<UpdateEmailDto>
    {
        public UpdateEmailDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
