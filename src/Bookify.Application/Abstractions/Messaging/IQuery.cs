using MediatR;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}