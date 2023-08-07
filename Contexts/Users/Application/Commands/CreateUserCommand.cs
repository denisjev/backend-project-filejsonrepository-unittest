using Contexts.Users.Application.DTOs;
using MediatR;
using Contexts.Users.Domain;

namespace Contexts.Users.Application.Command;

public record CreateUserCommand: IRequest<UserDTO>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string Image { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDTO>
{
    private readonly IUserRepository _userContext;

    public CreateUserCommandHandler(IUserRepository userContext)
    {
        _userContext = userContext;
    }
    public async Task<UserDTO> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var newUser = new User(Guid.NewGuid(), command.Name, command.Email, command.Country, command.Image);
        
        if (!await _userContext.Add(newUser))
            return null;

        return new UserDTO(newUser.Id.ToString(), newUser.Name, newUser.Email, newUser.Country,
            newUser.Image);
    }
}
