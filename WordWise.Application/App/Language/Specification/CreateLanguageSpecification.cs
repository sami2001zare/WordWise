using WordWise.Application.App.Language.Commands.CreateLanguage;

namespace WordWise.Application.App.Language.Specification;

public sealed class CreateLanguageSpecification(CreateLanguageCommand validationParams) : Specification<Core.Language.Language>
{
    public override bool IsSatisfiedBy(Core.Language.Language entity)
    {
        return entity.NativeTitle == validationParams.NativeTitle || entity.Title == validationParams.Title || entity.Abbrivation == validationParams.Abbr;
    }
}