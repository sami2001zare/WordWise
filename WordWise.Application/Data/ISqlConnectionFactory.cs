using System.Data;

namespace WordWise.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
