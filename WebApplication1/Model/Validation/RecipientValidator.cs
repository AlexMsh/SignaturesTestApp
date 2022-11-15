using FluentValidation;
using Signarutes.Domain.Contracts.models;

namespace Sigtatures.Web.Validation
{
    public class RecipientValidator : AbstractValidator<Recipient>
    {
        public RecipientValidator()
        {
            RuleFor(m => m).NotNull();
            RuleFor(m => m.Email).EmailAddress();
            RuleFor(m => m.Name).NotNull().NotEmpty();
        }
    }
}
