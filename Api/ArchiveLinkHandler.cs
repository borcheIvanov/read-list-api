using MediatR;

namespace Api;

public class ArchiveLinkHandler(IRepository repository) : IRequestHandler<ArchiveLinkCommand>
{
    public async Task Handle(ArchiveLinkCommand request, CancellationToken cancellationToken)
    {
        var link = await repository.GetLink(request.Id, "newUser", cancellationToken);
        if (link is not null)
        {
            link.IsArchived = true;
            await repository.Update(link, cancellationToken);
        }
    }
}