using FluentValidation;
using Schmoli.ServiceTemplate.Resources;

namespace Schmoli.ServiceTemplate.Validators
{
    public class PrimaryItemSaveValidator : AbstractValidator<PrimaryItemSaveResource>
    {
        public PrimaryItemSaveValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .MinimumLength(3)
                                .MaximumLength(32);
        }

    }
}
