using Xunit;

namespace BlazorSorting.Tests;

public class ParametersTests
{
    [Fact]
    public void DefaultConstructorStartsAscendingWithNoSort()
    {
        var p = new Parameters();

        Assert.Null(p.SortBy);
        Assert.Equal(Direction.Ascending, p.Direction);
        Assert.Equal(0, p.CurrentPage);
    }

    [Fact]
    public void DefaultSortConstructorSetsSortAndFirstPage()
    {
        var p = new Parameters("Name");

        Assert.Equal("Name", p.SortBy);
        Assert.Equal(Direction.Ascending, p.Direction);
        Assert.Equal(1, p.CurrentPage);
    }

    [Fact]
    public void SetSortByOnSameColumnTogglesDirection()
    {
        var p = new Parameters("Name");

        p.SetSortBy("Name");
        Assert.Equal(Direction.Descending, p.Direction);

        p.SetSortBy("Name");
        Assert.Equal(Direction.Ascending, p.Direction);
    }

    [Fact]
    public void SetSortByOnSameColumnKeepsCurrentPage()
    {
        var p = new Parameters("Name");
        p.SetPage(3);

        p.SetSortBy("Name");

        Assert.Equal(3, p.CurrentPage);
    }

    [Fact]
    public void SetSortByOnNewColumnResetsDirectionAndPage()
    {
        var p = new Parameters("Name") { Direction = Direction.Descending };
        p.SetPage(3);

        p.SetSortBy("Date");

        Assert.Equal("Date", p.SortBy);
        Assert.Equal(Direction.Ascending, p.Direction);
        Assert.Equal(1, p.CurrentPage);
    }

    [Fact]
    public void SetPageSetsCurrentPage()
    {
        var p = new Parameters();

        p.SetPage(7);

        Assert.Equal(7, p.CurrentPage);
    }
}
