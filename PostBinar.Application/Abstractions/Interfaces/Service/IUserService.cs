using PostBinar.Application.Common.Models.Users;
using PostBinar.Domain.Abstraction;
using PostBinar.Domain.Users;

namespace PostBinar.Application.Abstractions.Interfaces.Service;

public interface IUserService
{
    Task<Result<UserId>> Register(
        string firstName , 
        string lastName, 
        string email, 
        string password, 
        int specializationId);

    Task<Result<AccessTokenResponse>> Login(string email, string password);
}
