using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id);
        Task<User?> GetByEmailAsync(string email);
        void Add(User user);
        void Delete(User user);
    }
}
