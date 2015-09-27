using FluentValidation;
using UrlShortener.WebApi.Models;

namespace UrlShortener.WebApi.Lib.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.Name)
                .NotEmpty()
                .WithMessage("Nome deve ser informado.");

            RuleFor(account => account.Email)
                .NotEmpty()
                .WithMessage("E-mail deve ser informado.");

            When(account => account.Id == 0, () =>
                RuleFor(account => account.ConfirmPassword)
                    .Equal(account => account.Password)
                    .WithMessage("Confirmação de senha deve ser igual a senha."));
        }
    }
}