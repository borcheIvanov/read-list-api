using MediatR;

namespace Api;

public record DeleteLinkCommand(string Id): IRequest;