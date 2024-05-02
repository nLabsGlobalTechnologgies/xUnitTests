using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using RealWorld.WebAPI.DTOs;
using RealWorld.WebAPI.Logging;
using RealWorld.WebAPI.Models;
using RealWorld.WebAPI.Repositories;
using RealWorld.WebAPI.Services;

namespace Users.API.Tests.Unit;

public class UserServiceTests
{
    //Arrange
    private readonly UserService userService; // System Under Stand _sut;
    private readonly IUserRepository userRepository = Substitute.For<IUserRepository>();
    private readonly ILoggerAdapter<UserService> logger = Substitute.For<ILoggerAdapter<UserService>>();
    private readonly CreateUserDto createUserDto = new("Cuma KÖSE", 37, new(1987, 08, 29));
    public UserServiceTests()
    {
        userService = new(userRepository, logger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        //Arrange
        userRepository.GetAllAsync().Returns(Enumerable.Empty<User>().ToList());

        //Act
        var result = await userService.GetAllAsync();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnsUsers_WhenSomeUserExist()
    {
        //Arrange
        var newUser1 = new User
        {
            Id = 1,
            Age = 37,
            Name = "Cuma KÖSE",
            DateOfBirth = new(1987, 8, 29)
        };

        var newUser2 = new User
        {
            Id = 1,
            Age = 35,
            Name = "Ali KÖSE",
            DateOfBirth = new(1989, 8, 29)
        };

        var users = new List<User>(){ newUser1, newUser2 };
        userRepository.GetAllAsync().Returns(users);

        //Act

        var result = await userService.GetAllAsync();

        //Assert
        result.Should().BeEquivalentTo(users);
        result.Should().HaveCount(2);
        result.Should().NotHaveCount(3);

    }

    [Fact]
    public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
    {
        //Arrange
        userRepository.GetAllAsync().Returns(Enumerable.Empty<User>().ToList());

        //Act
        await userService.GetAllAsync();

        //Assert
        logger.Received(1).LogInformation(Arg.Is("Tüm kullanýcýlar getiriliyor!"));
        logger.Received(1).LogInformation(Arg.Is("Tüm kullancý listesi çekildi"));
    }

    [Fact]
    public async Task GetAllAsync_ShouldLogMessageAnException_WhenExceptionIsTrown()
    {
        //Arrange
        var exception = new ArgumentException("An error was encountered while traversing the user list!");
        userRepository.GetAllAsync().Throws(exception);

        //Act
        var requestAction = async() => await userService.GetAllAsync();
        await requestAction.Should().ThrowAsync<ArgumentException>();

        //Assert
        logger.Received(1).LogError(Arg.Is(exception), Arg.Is("An error was encountered while traversing the user list!"));
    }

    [Fact]
    public async Task CreateAsync_ShouldThrownAnError_WhenUserCreateDetailAreNotValid()
    {
        //Arrange

        //Act
        var action = async () => await userService.CreateAsync(createUserDto);

        //Assert
        await action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateAsync_ShouldThrownAnError_WhenUserNameExist()
    {
        //Arrange
        userRepository.NameIsExists(Arg.Any<string>()).Returns(true);

        //Act
        var action = async () => await userService.CreateAsync(new("Cuma KÖSE", 37, new (1987, 08, 29)));

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public void CreateAsync_ShouldCreateUserDtoToUserObject()
    {
        //Arrange

        //Atc
        var user = userService.CreateUserDtoToUserObject(createUserDto);

        //Assert
        user.Name.Should().Be(createUserDto.Name);
        user.Age.Should().Be(createUserDto.Age);
        user.DateOfBirth.Should().Be(createUserDto.DateOfBirth);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValidAndUnique()
    {
        //Arrange
        userRepository.NameIsExists(createUserDto.Name).Returns(false);
        userRepository.CreateAsync(Arg.Any<User>()).Returns(true);

        //Act
        var result = await userService.CreateAsync(createUserDto);

        //Assert
        result.Should().Be(true);
    }

    [Fact]
    public async Task CreateAsync_ShouldLogMessages_WhenInvoked()
    {
        //Arrange
        userRepository.NameIsExists(createUserDto.Name).Returns(false);
        userRepository.CreateAsync(Arg.Any<User>()).Returns(true);

        //Act
        await userService.CreateAsync(createUserDto);

        //Assert
        logger.Received(1).LogInformation(Arg.Is("Registration of user named UserName: {0} has started."), Arg.Any<string>());
        logger.Received(1).LogInformation(Arg.Is("UserId: The user {0} was created in {1} ms"), Arg.Any<int>(), Arg.Any<long>());
    }

    [Fact]
    public async Task CreateAsync_ShouldLogMessagesAndException_WhenExceptionIsThrown()
    {
        //Arrange
        var exception = new ArgumentException("We encountered an error during user registration!");
        userRepository.CreateAsync(Arg.Any<User>()).Throws(exception);

        //Act
        var action = async () => await userService.CreateAsync(createUserDto);

        //Assert
        await action.Should()
            .ThrowAsync<ArgumentException>();

        logger.Received(1).LogError(Arg.Is(exception), Arg.Is("We encountered an error during user registration!"));
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldThrownAnError_WhenUserNotExist()
    {
        //Arrange
        var userId = 1;
        userRepository.GetByIdAsync(userId).ReturnsNull();

        //Act
        var action = async () => await userService.DeleteByIdAsync(userId);

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExist()
    {
        //Arrange
        var userId = 1;
        var user = new User()
        {
            Id = userId,
            Name = "Cuma KÖSE",
            Age = 37,
            DateOfBirth = new(1987,08,29)
        };
        userRepository.GetByIdAsync(userId).Returns(user);
        userRepository.DeleteAsync(user).Returns(true);

        //Act
        var result = await userService.DeleteByIdAsync(userId);

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldLogMessages_WhenInvoked()
    {
        //Arrange
        var userId = 1;
        var user = new User()
        {
            Id = userId,
            Name = "Cuma KÖSE",
            Age = 37,
            DateOfBirth = new(1987, 08, 29)
        };
        userRepository.GetByIdAsync(userId).Returns(user);
        userRepository.DeleteAsync(user).Returns(true);
        //Act
        await userService.DeleteByIdAsync(userId);

        //Assert
        logger.Received(1).LogInformation(Arg.Is("UserId : {0} is deletion"), Arg.Is(userId));
        logger.Received(1).LogInformation(Arg.Is("User record with UserId: {0} was deleted in {1} ms!"), Arg.Is(userId), Arg.Any<long>());
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldLogMessagesAndException_WhenExceptionIsThrown()
    {
        //Arrange
        var userId = 1;
        var user = new User()
        {
            Id = userId,
            Name = "Cuma KÖSE",
            Age = 37,
            DateOfBirth = new(1987, 08, 29)
        };
        userRepository.GetByIdAsync(userId).Returns(user);
        var exception = new ArgumentException("We encountered an error deleting the user!");
        userRepository.DeleteAsync(user).Throws(exception);

        //Act
        var action = async () => await userService.DeleteByIdAsync(userId);

        //Assert
        await action.Should().ThrowAsync<ArgumentException>();
        logger.Received(1).LogError(Arg.Is(exception), Arg.Is("We encountered an error deleting the user!"));
    }
}