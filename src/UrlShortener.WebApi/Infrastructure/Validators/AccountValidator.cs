using FluentValidation;

namespace UrlShortener.WebApi.Infrastructure.Validators
{
    public class AccountValidator : AbstractValidator<Models.Account.Post.Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.Name)
                .NotEmpty()
                .WithMessage("Nome deve ser informado.");

            RuleFor(account => account.Email)
                .NotEmpty()
                .WithMessage("E-mail deve ser informado.");

            RuleFor(account => account.ConfirmPassword)
                .Equal(account => account.Password)
                .WithMessage("Confirmação de senha deve ser igual a senha.");
        }
    }
}