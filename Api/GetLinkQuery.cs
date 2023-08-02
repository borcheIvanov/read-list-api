using MediatR;

namespace Api;

public record GetLinkQuery(
	string Id,
	string UserId) : IRequest<Link?>;