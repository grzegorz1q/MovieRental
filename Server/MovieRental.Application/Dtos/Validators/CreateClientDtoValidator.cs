using FluentValidation;
using MovieRental.Application.Dtos.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Application.Dtos.Validators
{
    public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientDtoValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
