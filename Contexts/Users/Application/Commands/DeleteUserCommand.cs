using Contexts.Users.Domain;
using MediatR;

namespace Contexts.Users.Application.Command;

public class DeleteUserCommand: IRequest<bool>
{
    public string Id { get; set; }
}

public class DeleteUserCommandHandler: IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _userContext;

    public DeleteUserCommandHandler(IUserRepository userContext)
    {
        _userContext = userContext;
    }
    public async Task<bool> Handle(DeleteUserCommand comand, CancellationToken cancellationToken)
    {
        var userItem = await _userContext.GetById(comand.Id);
        if (userItem == null)
            return false;

        if(await _userContext.Delete(userItem))
            return true;

        return false;
    }
}