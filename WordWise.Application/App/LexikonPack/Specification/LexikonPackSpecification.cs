using WordWise.Application.App.LexikonPack.Commands.Create;

namespace WordWise.Application.App.LexikonPack.Specification;

public sealed class LexikonPackSpecification(CreateLexikonPackCommand validation) : Specification<Core.Lexikon.LexikonPack>
{
    public override bool IsSatisfiedBy(Core.Lexikon.LexikonPack entity)
    {
        return entity.Title == validation.Title || entity.Language.Title == validation.Language;
    }
}
