using FluentValidation;
using BCP.ExchangeRate.Domain;

namespace BCP.ExchangeRate.WebAPI.Validators
{
    public class PostExchangeRateDtoValidator : AbstractValidator<PostExchangeRateDto>
    {
        public PostExchangeRateDtoValidator()
        {
            RuleFor(x => x.OriginCurrency).NotEmpty().MaximumLength(3);
            RuleFor(x => x.DestinationCurrency).NotEmpty().MaximumLength(3);
            RuleFor(x => x.ExchangeRate).NotNull();
        }
    }
}
