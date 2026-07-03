using Microsoft.EntityFrameworkCore;
using WordWise.Core.User;
using WordWise.Core.User.Repositpry;
using WordWise.Infra.Data;


namespace WordWise.Infra.Repositories;

internal sealed class JsonWebTokenRepository(WordWiseDbContext dbContext) : IJsonWebTokenRepository
{
    public async Task AddAsync(JsonWebToken jwt, CancellationToken cancellationToken = default)
    {
        await dbContext.JsonWebTokens.AddAsync(jwt, cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<JsonWebToken> jwts, CancellationToken cancellationToken = default)
    {
        await dbContext.JsonWebTokens.AddRangeAsync(jwts, cancellationToken);
    }

    public async Task<JsonWebToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.JsonWebTokens.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<JsonWebToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await dbContext.JsonWebTokens.FirstOrDefaultAsync(i => i.Token == token, cancellationToken);
    }
}