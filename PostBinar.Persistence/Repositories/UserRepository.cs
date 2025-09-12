using Microsoft.EntityFrameworkCore;
using PostBinar.Application.Repositories;
using PostBinar.Domain.Users;
using PostBinar.Persistence.DbContects;

namespace PostBinar.Persistence.Repositories;

internal sealed class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(PostBinarDbContext context) : base(context) { }
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
