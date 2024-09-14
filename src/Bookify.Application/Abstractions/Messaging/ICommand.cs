using Bookify.Domain.Abstractions;
using MediatR;

namespace Bookify.Application.Abstractions.Messaging;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface IBaseCommand
{
}