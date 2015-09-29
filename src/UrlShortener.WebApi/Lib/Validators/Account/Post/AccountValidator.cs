using FluentValidation;
using Model = UrlShortener.WebApi.Models.Account.Post;

namespace UrlShortener.WebApi.Lib.Validators.Account.Post
{
    public class AccountValidator : AbstractValidator<Model.Account>
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