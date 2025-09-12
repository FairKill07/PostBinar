using System.Data;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IUserService
{
    Task<UserId> Register(
        string firstName , 
        string lastName, 
        string email, 
        string passwordHash, 
        int specializationId);
    Task<string> Login(string email, string password);
}
