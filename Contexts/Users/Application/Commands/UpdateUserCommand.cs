using Contexts.Users.Domain;
using MediatR;

namespace Contexts.Users.Application.Command;

public class UpdateUserCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string Image { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userContext;

    public UpdateUserCommandHandler(IUserRepository userContext)
    {
        _userContext = userContext;
    }

    public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var updateUser = new User(Guid.Parse(command.Id), command.Name, command.Email, command.Country, command.Image);

        if (await _userContext.Update(updateUser))
            return true;

        return false;
    }
}