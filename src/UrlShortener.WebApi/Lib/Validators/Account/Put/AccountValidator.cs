using FluentValidation;
using Model = UrlShortener.WebApi.Models.Account.Put;

namespace UrlShortener.WebApi.Lib.Validators.Account.Put
{
    public class AccountValidator : AbstractValidator<Model.Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.Name)
                .NotEmpty()
                .WithMessage("Nome deve ser informado.");

            RuleFor(account => account.Password)
                .NotNull()
                .WithMessage("Senha deve ser informada.");
        }
    }
}
