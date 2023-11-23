using MediatR;

namespace Api.Controllers;

public record ArchiveLinkCommand(string Id): IRequest;