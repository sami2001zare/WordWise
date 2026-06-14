using WordWise.Framework;
using MediatR;

namespace WordWise.Application.Messaging.Command;

public interface ICortexCommand : IRequest<Result>, IBaseCommand;

public interface ICortexCommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;
