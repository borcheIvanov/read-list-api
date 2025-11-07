using MediatR;

namespace Api;

public class GetLinkHandler(IRepository repository) : IRequestHandler<GetLinkQuery, Link?>
{
	public async Task<Link?> Handle(GetLinkQuery request, CancellationToken cancellationToken)
	{
		return await repository.GetLink(request.Id, request.UserId, cancellationToken);
	}
}