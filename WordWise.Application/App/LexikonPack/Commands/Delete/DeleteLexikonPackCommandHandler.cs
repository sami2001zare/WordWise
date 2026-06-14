using WordWise.Application.Messaging.Command;
using WordWise.Core.Lexikon;
using WordWise.Core.Lexikon.Repository;
using WordWise.Framework;
using WordWise.Framework.Repository;

namespace WordWise.Application.App.LexikonPack.Commands.Delete;

internal sealed class DeleteLexikonPackCommandHandler(
    ILexikonPackRepository _lexikonPackRepository,
    IUnitOfWork _unitOfWork
    ) : ICortexCommandHandler<DeleteLexikonPackCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteLexikonPackCommand request, CancellationToken cancellationToken)
    {
        Core.Lexikon.LexikonPack? lexikonpack = await _lexikonPackRepository.GetByIdAsync(request.Id, cancellationToken);

        if (lexikonpack is null)
        {
            return Result.Failure<Guid>(new Error("Not Exists", "Already An Item Like This Exists In The System"));
        }

        try
        {
            _unitOfWork.Remove(lexikonpack);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return lexikonpack.Id;
        }
        catch (Exception)
        {
            return Result.Failure<Guid>(new Error("System Error", "Error Happened While Saving Data"));
        }
    }
}
