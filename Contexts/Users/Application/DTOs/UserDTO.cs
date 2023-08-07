namespace Contexts.Users.Application.DTOs;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string Image { get; set; }

    public UserDTO()
    {
    }

    public UserDTO(string id, string name, string email, string country, string image)
    {
        Id = id;
        Name = name;
        Email = email;
        Country = country;
        Image = image;
    }
}