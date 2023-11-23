using System.Net;
using Microsoft.Azure.Cosmos;

namespace Api;

public class Repository : IRepository
{
    private readonly Container _container;

    public Repository(Container container)
    {
        _container = container ?? throw new ArgumentNullException(nameof(container));
    }


    public async Task<IEnumerable<Link>> GetLinks(string userId, CancellationToken ct)
    {
        var query = _container.GetItemQueryIterator<Link>(
            new QueryDefinition("SELECT * FROM l " +
                                "WHERE l.is_archived = @p1")
                .WithParameter("@p1", false)
        );

        var results = new List<Link>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync(ct);
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task<Link?> GetLink(string id, string userId, CancellationToken ct)
    {
        try
        {
            var response = await _container.ReadItemAsync<Link>(id, new PartitionKey(userId), cancellationToken: ct);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task Create(Link link, CancellationToken ct)
    {
        await _container.CreateItemAsync(link, new PartitionKey(link.UserId), cancellationToken: ct);
    }

    public async Task Update(Link link, CancellationToken ct)
    {
        await _container.ReplaceItemAsync(link, link.Id, new PartitionKey("newUser"), cancellationToken: ct);
    }

    public async Task Delete(string requestId, CancellationToken cancellationToken)
    {
        await _container.DeleteItemAsync<Link>(requestId, new PartitionKey("newUser"),
            cancellationToken: cancellationToken);
    }
}