using FluentValidation;
using MovieRental.Application.Dtos.Client;

namespace MovieRental.Application.Dtos.Validators
{
    public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.PhoneNumber)
                .Must(p => p.ToString().Length == 9)
                .WithMessage("Numer telefonu musi mieć 9 cyfr");
        }
    }
}
