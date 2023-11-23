using Api.Controllers;
using MediatR;

namespace Api;

public class ArchiveLinkHandler: IRequestHandler<ArchiveLinkCommand>
{
    private readonly IRepository _repository;

    public ArchiveLinkHandler(IRepository repository)
    {
        _repository = repository;
    }


    public async Task Handle(ArchiveLinkCommand request, CancellationToken cancellationToken)
    {
        var link = await _repository.GetLink(request.Id, "newUser", cancellationToken);
        if (link is not null)
        {
            link.IsArchived = true;
            await _repository.Update(link, cancellationToken);
        }
    }
}