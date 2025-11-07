using MediatR;

namespace Api;

public record ArchiveLinkCommand(string Id): IRequest;