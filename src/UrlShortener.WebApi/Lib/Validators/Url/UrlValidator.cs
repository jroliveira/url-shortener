using FluentValidation;
using Model = UrlShortener.WebApi.Models.Url.Post;

namespace UrlShortener.WebApi.Lib.Validators.Url
{
    public class UrlValidator : AbstractValidator<Model.Url>
    {
        public UrlValidator()
        {
            RuleFor(url => url.Address)
                .NotEmpty()
                .WithMessage("Endereço deve ser informado.");

            RuleFor(url => url.Account)
                .NotNull()
                .WithMessage("Conta deve ser informada.");

            When(url => url.Account != null, () =>
                RuleFor(url => url.Account.Id)
                    .GreaterThan(0)
                    .WithMessage("Id da conta deve ser maior que zero."));
        }
    }
}
