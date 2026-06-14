using WordWise.Framework;
using MediatR;

namespace WordWise.Application.Messaging.Command;

public interface ICortexCommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICortexCommand;

public interface ICortexCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICortexCommand<TResponse>;