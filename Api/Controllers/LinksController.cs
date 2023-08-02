using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinksController: ControllerBase
{
	private readonly ISender _sender;

	public LinksController(ISender sender)
	{
		_sender = sender;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetLinks(CancellationToken cancellationToken)
	{
		var query = new GetLinksQuery("newUser");
		var result = await _sender.Send(query, cancellationToken);
		return Ok(result);
	}

	[HttpPost]
	public IActionResult CreateLink([FromBody]CreateLinkRequest cl, CancellationToken cancellationToken)
	{
		var id = Guid.NewGuid().ToString();
		var command = new CreateLinkCommand(id, new Uri(cl.Link), "newUser");
		_sender.Send(command, cancellationToken);
		return Ok();
	}

	[HttpPut]
	public IActionResult Archive([FromBody] Guid id, CancellationToken cancellationToken)
	{
		return Ok();
	}
	
	[HttpPatch]
	public IActionResult MarkAsRead([FromBody] Guid id, CancellationToken cancellationToken)
	{
		return Ok();
	}
}

public class CreateLinkRequest
{
	public string Link { get; set; } = null!;
}