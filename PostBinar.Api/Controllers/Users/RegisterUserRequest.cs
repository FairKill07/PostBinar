namespace PostBinar.Api.Controllers.Users;

public sealed record RegisterUserRequest (
    string FirstName,
    string LastName, 
    string Email, 
    string Password, 
    int SpecializationId) { };
