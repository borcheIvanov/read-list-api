using MediatR;

namespace Api;

public class GetLinksHandler(IRepository repository) : IRequestHandler<GetLinksQuery, IEnumerable<Link>>
{
	public async Task<IEnumerable<Link>> Handle(GetLinksQuery request, CancellationToken cancellationToken)
	{
		return await repository.GetLinks(request.UserId, cancellationToken);
	}
}