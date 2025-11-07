using MediatR;

namespace Api;

public class DeleteLinkHandler(IRepository repository) : IRequestHandler<DeleteLinkCommand>
{
    public async Task Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
    {
        await repository.Delete(request.Id, cancellationToken);
    }
}