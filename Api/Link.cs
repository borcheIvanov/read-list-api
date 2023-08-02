using Newtonsoft.Json;

namespace Api;

public class Link
{
	[JsonProperty("id")] 
	public string Id { get; set; } = null!;
	[JsonProperty("address")]
	public string Address { get; set; } = null!;
	[JsonProperty("title")]
	public string Title { get; set; } = null!;
	[JsonProperty("is_read")]
	public bool IsRead { get; set; }
	[JsonProperty("is_archived")]
	public bool IsArchived { get; set; }
	[JsonProperty("user_id")]
	public string UserId { get; set; } = "newUser";
}