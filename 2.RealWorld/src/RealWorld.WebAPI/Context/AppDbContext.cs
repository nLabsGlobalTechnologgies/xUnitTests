using Microsoft.EntityFrameworkCore;
using RealWorld.WebAPI.Models;

namespace RealWorld.WebAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
