using JetBrains.Annotations;
using MediatR;

namespace Api;

[PublicAPI]
public record GetLinkQuery(
	string Id,
	string UserId) : IRequest<Link?>;