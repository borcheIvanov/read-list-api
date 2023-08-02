using System;
using Api;
using Moq;
using Xunit;

namespace Tests;

public class CreateLinkHandlerTest
{
    [Fact]
    public void Scrape_title()
    {
        var mockRepo = new Mock<IRepository>();
        var mockScraper = new Mock<IContentScraper>();
        mockScraper.Setup(s => s.GetContent(It.IsAny<Uri>()))
            .Returns("<html><head><title>the title</title></head><body>body content</body></html>");
        
        var handler = new CreateLinkHandler(mockRepo.Object) {
            Scraper = mockScraper.Object
        };
        var title =  handler.GetTitle(new Uri("https://www.google.com"));
        
        Assert.Equal("the title", title);
    }
    
    [Fact]
    public void Scrape_title_fallback_to_absolute_uri()
    {
        var mockRepo = new Mock<IRepository>();
        var mockScraper = new Mock<IContentScraper>();
        mockScraper.Setup(s => s.GetContent(It.IsAny<Uri>()))
            .Returns("<html><head></head><body>body content</body></html>");
        
        var handler = new CreateLinkHandler(mockRepo.Object) {
            Scraper = mockScraper.Object
        };
        var title =  handler.GetTitle(new Uri("https://www.google.com/blog/post"));
        
        Assert.Equal("https://www.google.com/blog/post", title);
    }
}