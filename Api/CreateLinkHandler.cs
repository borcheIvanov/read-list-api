using HtmlAgilityPack;
using MediatR;

namespace Api;

public class CreateLinkHandler: IRequestHandler<CreateLinkCommand>
{
	private readonly IRepository _repository;
	public IContentScraper Scraper;

	public CreateLinkHandler(IRepository repository)
	{
		_repository = repository;
		Scraper = new ContentScraper();
	}
	
	public async Task Handle(CreateLinkCommand request, CancellationToken cancellationToken)
	{
		var link = new Link {
			Id = request.Id,
			UserId = request.UserId,
			Title = GetTitle(request.Link),
			Address = request.Link.AbsoluteUri
		};
		await _repository.Create(link, cancellationToken);
	}

	public string GetTitle(Uri requestLink)
	{
		var htmlContent = Scraper.GetContent(requestLink);
		var htmlDocument = new HtmlDocument();
		htmlDocument.LoadHtml(htmlContent);
		var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//head/title");

		var title = titleNode?.InnerText ?? requestLink.AbsoluteUri;
		return title;
	}
}

public interface IContentScraper
{
	string GetContent(Uri uri);
}

public class ContentScraper : IContentScraper
{
	public string GetContent(Uri uri)
	{
		var web = new HtmlWeb();
		var doc = web.Load(uri.AbsoluteUri);
		
		return doc.DocumentNode.OuterHtml;
	}
}