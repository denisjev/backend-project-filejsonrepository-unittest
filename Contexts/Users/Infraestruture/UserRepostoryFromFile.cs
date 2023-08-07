using Newtonsoft.Json;
using Contexts.Users.Domain;

namespace Contexts.Users.Infraestruture;

public class UserRepostoryFromFile : IUserRepository
{
    private readonly string _usersFile = @".\Data\UsersData.json";

    private async Task<List<User>>? GetListJsonFromFile()
    {
        try
        {        
            List<User> users = new List<User>();

            if (File.Exists(_usersFile))
            {
                string content = await File.ReadAllTextAsync(_usersFile);
                users = JsonConvert.DeserializeObject<List<User>>(content);
            }
            return users;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<User>();
        }
    }

    private async Task<bool> SaveListJsonToFile(IEnumerable<User> _users)
    {
        try
        {        
            if (File.Exists(_usersFile))
                await File.WriteAllTextAsync(_usersFile, JsonConvert.SerializeObject(_users));
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    public async Task<List<User>> GetAll() 
    {
        return await GetListJsonFromFile();
    }

    public async Task<User> GetById(string id)
    {
        try
        {
            IEnumerable<User>? _users = await GetListJsonFromFile();

            User userFound = (from user in _users
                where user.Id.ToString() == id
                select user).First();

            return userFound;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> Add(User user)
    {
        try
        {
            IEnumerable<User>? _users = await GetListJsonFromFile();
            _users = _users.Append(user);
            if(await SaveListJsonToFile(_users))
                return true;
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public async Task<bool> Update(User user)
    {
        try
        {
            IEnumerable<User>? _users = await GetListJsonFromFile();

            foreach (User current in _users)
            {
                if (current.Id == user.Id)
                {
                    current.Name = user.Name;
                    current.Email = user.Email;
                    current.Country = user.Country;
                    current.Image = user.Image;
                    
                    return await SaveListJsonToFile(_users);
                }
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    
    public async Task<bool> Delete(User user)
    {
        try
        {
            IEnumerable<User>? _users = await GetListJsonFromFile();
            var _usersToList = _users.ToList();
            bool isDelete = _usersToList.Remove(_usersToList.First(u => u.Id == user.Id));
            
            if(isDelete)
                await SaveListJsonToFile(_usersToList);

            return isDelete;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}