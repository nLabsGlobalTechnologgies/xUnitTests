using FluentValidation;
using RealWorld.WebAPI.DTOs;
using RealWorld.WebAPI.Logging;
using RealWorld.WebAPI.Models;
using RealWorld.WebAPI.Repositories;
using RealWorld.WebAPI.Validators;
using System.Diagnostics;

namespace RealWorld.WebAPI.Services;

public sealed class UserService(IUserRepository userRepository, ILoggerAdapter<UserService> logger) : IUserService
{
    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Tüm kullanıcılar getiriliyor!");
        try
        {
            return await userRepository.GetAllAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Kullanıcı listesi geçerken bir hatayla karşılaşıldı!");
            throw;
        }
        finally
        {
            logger.LogInformation("Tüm kullancı listesi çekildi");
        }
    }

    public async Task<bool> CreateAsync(CreateUserDto request, CancellationToken cancellationToken = default)
    {
		CreateUserDtoValidator validator = new();
		var validationResult = validator.Validate(request);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(string.Join(", ", validationResult.Errors.Select(s => s.ErrorMessage)));
		}

		var nameIsExists = await userRepository.NameIsExists(request.Name, cancellationToken);
		if (nameIsExists)
		{
			throw new ArgumentException("This name has been recorded before!");
        }

        var user = CreateUserDtoToUserObject(request);
		logger.LogInformation("Registration of user named UserName: {0} has started.", user.Name);
        var stopWatch = Stopwatch.StartNew();

        try
        {
            return await userRepository.CreateAsync(user, cancellationToken);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "We encountered an error during user registration!");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            logger.LogInformation("UserId: The user {0} was created in {1} ms", user.Id, stopWatch.ElapsedMilliseconds);
        }
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
        {
            throw new ArgumentException("User not found!");
        }
        logger.LogInformation("UserId : {0} is deletion", user.Id);
        var stopWatch = Stopwatch.StartNew();
        try
        {
            return await userRepository.DeleteAsync(user, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "We encountered an error deleting the user!");
            throw;
        }
        finally
        {
            stopWatch.Stop();
            logger.LogInformation("User record with UserId: {0} was deleted in {1} ms!", user.Id, stopWatch.ElapsedMilliseconds);
        }
    }

    public User CreateUserDtoToUserObject(CreateUserDto request)
    {
        return new User()
		{
			Name = request.Name,
			Age = request.Age,
			DateOfBirth = request.DateOfBirth
		};
    }
}
