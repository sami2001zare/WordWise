namespace WordWise.Application.App.LexikonPack.Specification;

public sealed class GetLanguageByTitleSpecification(string Title) : Specification<Core.Language.Language>
{
    public override bool IsSatisfiedBy(Core.Language.Language entity)
    {
        return entity.Title == Title;
    }
}
