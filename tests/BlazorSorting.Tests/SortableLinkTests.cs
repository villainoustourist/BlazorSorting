using Bunit;
using Xunit;

namespace BlazorSorting.Tests;

public class SortableLinkTests : BunitContext
{
    private IRenderedComponent<SortableLink> RenderLink(Parameters s, string column, string? displayText = null,
        Func<string, Task>? onSort = null) =>
        Render<SortableLink>(ps => ps
            .Add(p => p.S, s)
            .Add(p => p.ColumnName, column)
            .Add(p => p.DisplayText, displayText)
            .Add(p => p.OnSort, onSort));

    [Fact]
    public void ShowsColumnNameWhenNoDisplayText()
    {
        var cut = RenderLink(new Parameters("Name"), "Date");

        Assert.Equal("Date", cut.Find("a").TextContent.Trim());
    }

    [Fact]
    public void ShowsDisplayTextWhenSet()
    {
        var cut = RenderLink(new Parameters("Name"), "Date", "Order date");

        Assert.Equal("Order date", cut.Find("a").TextContent.Trim());
    }

    [Fact]
    public void ShowsNeutralIconWhenSortedByAnotherColumn()
    {
        var cut = RenderLink(new Parameters("Name"), "Date");

        var icon = cut.Find("a i");
        Assert.Contains("fa-sort", icon.ClassList);
        Assert.Contains("text-light", icon.ClassList);
    }

    [Fact]
    public void ShowsUpIconWhenSortedAscending()
    {
        var cut = RenderLink(new Parameters("Date"), "Date");

        var icon = cut.Find("a i");
        Assert.Contains("fa-sort-up", icon.ClassList);
        Assert.Contains("text-dark", icon.ClassList);
    }

    [Fact]
    public void ShowsDownIconWhenSortedDescending()
    {
        var cut = RenderLink(new Parameters("Date") { Direction = Direction.Descending }, "Date");

        Assert.Contains("fa-sort-down", cut.Find("a i").ClassList);
    }

    [Fact]
    public void ClickRaisesOnSortWithColumnName()
    {
        string? sorted = null;
        var cut = RenderLink(new Parameters("Name"), "Date", "Order date", c => { sorted = c; return Task.CompletedTask; });

        cut.Find("a").Click();

        Assert.Equal("Date", sorted);
    }

    [Fact]
    public void ClickWithoutHandlerDoesNotThrow()
    {
        var cut = RenderLink(new Parameters("Name"), "Date");

        cut.Find("a").Click();
    }

    [Fact]
    public void IconFollowsParametersAfterToggleAndRerender()
    {
        var s = new Parameters("Date");
        var cut = RenderLink(s, "Date", onSort: c => { s.SetSortBy(c); return Task.CompletedTask; });

        cut.Find("a").Click();
        cut.Render();

        Assert.Contains("fa-sort-down", cut.Find("a i").ClassList);
    }
}
