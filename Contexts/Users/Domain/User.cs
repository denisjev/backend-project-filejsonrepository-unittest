namespace Contexts.Users.Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string Image { get; set; }

    public User()
    {
    }

    public User(Guid id, string name, string email, string country, string image)
    {
        Id = id;
        Name = name;
        Email = email;
        Country = country;
        Image = image;
    }
}