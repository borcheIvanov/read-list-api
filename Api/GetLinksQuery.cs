using MediatR;

namespace Api;

public record GetLinksQuery(string UserId) : IRequest<IEnumerable<Link>>;