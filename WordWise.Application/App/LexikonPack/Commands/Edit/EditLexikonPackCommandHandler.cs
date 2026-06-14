using WordWise.Application.Clock;
using WordWise.Application.Messaging.Command;
using WordWise.Core.Lexikon.Repository;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.LexikonPack.Commands.Edit;

internal sealed class EditLexikonPackCommandHandler(
    ILexikonPackRepository _lexikonPackRepository,
    IDateTimeProvider _dateTimeProvider,
    IUnitOfWork _unitOfWork
    ) : ICortexCommandHandler<EditLexikonPackCommand>
{
    public async Task<Result> Handle(EditLexikonPackCommand request, CancellationToken cancellationToken)
    {
        Core.Lexikon.LexikonPack? lexikonpack = await _lexikonPackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lexikonpack is null)
        {
            return Result.Failure(new Error("Not Exists", "Already An Item Like This Exists In The System"));
        }

        if (lexikonpack.Title == request.Title)
        {
            return Result.Failure(new Error("Exists", "Already An Item Like This Exists In The System"));
        }


        try
        {
            lexikonpack.SetTitle(request.Title);
            lexikonpack.ModifiedDateTime = _dateTimeProvider.UtcNow;

            _unitOfWork.Update(lexikonpack);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(new Error("System Error", "Error Happened While Saving Data"));
        }
    }
}
