using CallCenter.Client.Pages;

namespace ClientTests;

public class CounterTests
{
    [Fact]
    public void CounterShouldIncrementWhenClicked()
    {
        // Arrange: render the Counter.razor component
        using TestContext ctx = new();
        IRenderedComponent<Counter> cut = ctx.RenderComponent<Counter>();

        // Act: find and click the <button> element to increment
        // the counter in the <p> element
        cut.Find("button").Click();

        // Assert: first find the <p> element, then verify its content
        cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 1</p>");
    }
}