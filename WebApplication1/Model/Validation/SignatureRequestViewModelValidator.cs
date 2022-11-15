using FluentValidation;
using Signarutes.Domain.Contracts.models.Request;

namespace Sigtatures.Web.Validation
{
    public class SignatureRequestValidator : AbstractValidator<SignatureRequest>
    {
        public SignatureRequestValidator()
        {
            RuleFor(m => m.File).NotNull();
            RuleFor(m => m.Recipient).SetValidator(new RecipientValidator());
        }
    }
}
