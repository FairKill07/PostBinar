using System.Data;

namespace PostBinar.Application.Abstractions.Interfaces;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
