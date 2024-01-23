using System;
using Api;
using NSubstitute;
using Xunit;

namespace Tests;

public class CreateLinkHandlerTest
{
    [Fact]
    public void Scrape_title()
    {
        var mockRepo = Substitute.For<IRepository>();
        var mockScraper = Substitute.For<IContentScraper>();
        mockScraper.GetContent(Arg.Any<Uri>())
            .Returns("<html><head><title>the title</title></head><body>body content</body></html>");
        
        var handler = new CreateLinkHandler(mockRepo) {
            Scraper = mockScraper
        };
        var title =  handler.GetTitle(new Uri("https://www.google.com"));
        
        Assert.Equal("the title", title);
    }
    
    [Fact]
    public void Scrape_title_fallback_to_absolute_uri()
    {
        var mockRepo = Substitute.For<IRepository>();
        var mockScraper = Substitute.For<IContentScraper>();
        mockScraper.GetContent(Arg.Any<Uri>())
            .Returns("<html><head></head><body>body content</body></html>");
        
        var handler = new CreateLinkHandler(mockRepo) {
            Scraper = mockScraper
        };
        var title =  handler.GetTitle(new Uri("https://www.google.com/blog/post"));
        
        Assert.Equal("https://www.google.com/blog/post", title);
    }
}