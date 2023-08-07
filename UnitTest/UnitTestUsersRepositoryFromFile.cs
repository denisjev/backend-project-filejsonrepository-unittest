using FluentAssertions;
using Contexts.Users.Infraestruture;
using Contexts.Users.Domain;

namespace UnitTest;

public class UnitTestUsersRepositoryFromFile
{
    [Fact]
    public async void GetAllUsers()
    {
        var _target = new UserRepostoryFromFile();

        var result = await _target.GetAll();

        result.Should().NotBeEmpty();
        result.Should().BeOfType<List<User>>();
    }
      
    [Fact]
    public async void GetUserById()
    {
        var _target = new UserRepostoryFromFile();
        var listUser = await _target.GetAll();
        var result = await _target.GetById(listUser[0].Id.ToString());

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
    }

    [Fact]
    public async void AddUser()
    {
        var _target = new UserRepostoryFromFile();
        var newUser = new User(
            Guid.NewGuid(),
            "Denis Espinoza",
            "denisjev@gmail.com",
            "Nicaragua",
            "https://example.com/myimage");
        
        var result = await _target.Add(newUser);

        result.Should().BeTrue();
    }
}