using WordWise.Framework;
using MediatR;

namespace WordWise.Application.Messaging.Query;

public interface ICortexQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : ICortexQuery<TResponse>;
