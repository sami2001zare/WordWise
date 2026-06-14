using WordWise.Framework;
using MediatR;

namespace WordWise.Application.Messaging.Query;

public interface ICortexQuery<TQueryResponse> : IRequest<Result<TQueryResponse>>;
