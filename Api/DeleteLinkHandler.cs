using MediatR;

namespace Api;

public class DeleteLinkHandler: IRequestHandler<DeleteLinkCommand>
{
    private readonly IRepository _repository;

    public DeleteLinkHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id, cancellationToken);
    }
}