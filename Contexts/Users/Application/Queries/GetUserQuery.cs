using Contexts.Users.Application.DTOs;
using MediatR;
using Contexts.Users.Domain;

namespace Contexts.Users.Application.Queries;

public class GetUserQuery : IRequest<UserDTO>
{
    public string Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
{
    private readonly IUserRepository _userContext;

    public GetUserQueryHandler(IUserRepository userContext)
    {
        _userContext = userContext;
    }

    public async Task<UserDTO> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userContext.GetById(query.Id);

        if (user != null)
            return new UserDTO
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Image = user.Image,
                Country = user.Country
            };

        return null;
    }
}
