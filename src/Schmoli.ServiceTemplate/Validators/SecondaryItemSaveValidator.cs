using FluentValidation;
using Schmoli.ServiceTemplate.Resources;

namespace Schmoli.ServiceTemplate.Validators
{
    public class SecondaryItemSaveValidator : AbstractValidator<SecondaryItemSaveResource>
    {
        public SecondaryItemSaveValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .MinimumLength(3)
                                .MaximumLength(32);
        }
    }
}
