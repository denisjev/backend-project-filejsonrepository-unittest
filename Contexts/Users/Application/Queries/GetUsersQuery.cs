using Contexts.Users.Application.DTOs;
using MediatR;
using Contexts.Users.Domain;

namespace Contexts.Users.Application.Queries;

public class GetUsersQuery : IRequest<List<UserDTO>>
{
    
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDTO>>
{
    private readonly IUserRepository _userContext;

    public GetUsersQueryHandler(IUserRepository userContext)
    {
        _userContext = userContext;
    }

    public async Task<List<UserDTO>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userContext.GetAll();
            return users.Select(s => new UserDTO()
                {
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Email = s.Email,
                    Image = s.Image,
                    Country = s.Country
                }
            ).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<UserDTO>();
        }
    }
}
