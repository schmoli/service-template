using FluentValidation;
using Schmoli.ServiceTemplate.Resources;

public class SecondaryItemSaveValidator : AbstractValidator<SecondaryItemSaveResource>
{
    public SecondaryItemSaveValidator()
    {
        RuleFor(x => x.Name).NotNull()
                            .MinimumLength(3)
                            .MaximumLength(32);
    }
}
