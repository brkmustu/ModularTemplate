using CoreModule.Application.Common.Contracts;
using CoreModule.Application.Common.Interfaces;
using System.Security;

namespace CoreModule.Application.Authorization;

public class AuthorizationQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    private readonly IQueryHandler<TQuery, TResult> decoratedHandler;
    private readonly ICurrentUserService currentUser;

    public AuthorizationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decoratedHandler, ICurrentUserService currentUser)
    {
        this.decoratedHandler = decoratedHandler;
        this.currentUser = currentUser;
    }

    private void Authorize()
    {
        if (!this.currentUser.IsInRole(nameof(TQuery)))
        {
            throw new SecurityException();
        }
    }

    public Task<TResult> Handle(TQuery query)
    {
        this.Authorize();

        return this.decoratedHandler.Handle(query);
    }
}
