using Microsoft.EntityFrameworkCore;
using RealWorld.WebAPI.Context;
using RealWorld.WebAPI.Models;

namespace RealWorld.WebAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Remove(user);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.Users.ToListAsync(cancellationToken);
        return result;
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return user;
    }

    public async Task<bool> NameIsExists(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Update(user);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}
