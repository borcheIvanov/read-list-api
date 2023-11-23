namespace Api;

public interface IRepository
{
	public Task<IEnumerable<Link>> GetLinks(string userId, CancellationToken ct);
	public Task<Link?> GetLink(string id, string userId, CancellationToken ct);
	public Task Create(Link link, CancellationToken ct);
	public Task Update(Link link, CancellationToken ct);
	public Task Delete(string requestId, CancellationToken cancellationToken);
}