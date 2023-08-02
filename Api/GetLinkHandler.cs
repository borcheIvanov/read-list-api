using MediatR;

namespace Api;

public class GetLinkHandler: IRequestHandler<GetLinkQuery, Link?>
{
	private readonly IRepository _repository;

	public GetLinkHandler(IRepository repository)
	{
		_repository = repository;
	}
	
	public async Task<Link?> Handle(GetLinkQuery request, CancellationToken cancellationToken)
	{
		return await _repository.GetLink(request.Id, request.UserId, cancellationToken);
	}
}