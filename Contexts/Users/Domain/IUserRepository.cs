namespace Contexts.Users.Domain;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User> GetById(string id);
    Task<bool> Add(User user);
    Task<bool> Update(User user);
    Task<bool> Delete(User user);
}