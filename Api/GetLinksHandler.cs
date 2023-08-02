using MediatR;

namespace Api;

public class GetLinksHandler: IRequestHandler<GetLinksQuery, IEnumerable<Link>>
{
	private readonly IRepository _repository;

	public GetLinksHandler(IRepository repository)
	{
		_repository = repository;
	}
	
	public async Task<IEnumerable<Link>> Handle(GetLinksQuery request, CancellationToken cancellationToken)
	{
		return await _repository.GetLinks(request.UserId, cancellationToken);
	}
}