using MediatR;

namespace Api;

public record CreateLinkCommand(
	string Id,
	Uri Link, 
	string UserId) : IRequest;