using Xunit;

namespace BlazorSorting.Tests;

public class ExtensionsTests
{
    public record Row(int Id, string Name);

    private static IQueryable<Row> Rows() => new[]
    {
        new Row(2, "Bravo"),
        new Row(3, "Alpha"),
        new Row(1, "Charlie"),
    }.AsQueryable();

    [Fact]
    public void OrderByColumnAscending()
    {
        var ids = Rows().OrderBy("Id", Direction.Ascending).Select(r => r.Id);

        Assert.Equal(new[] { 1, 2, 3 }, ids);
    }

    [Fact]
    public void OrderByColumnDescending()
    {
        var names = Rows().OrderBy("Name", Direction.Descending).Select(r => r.Name);

        Assert.Equal(new[] { "Charlie", "Bravo", "Alpha" }, names);
    }

    [Fact]
    public void OrderByParameters()
    {
        var parameters = new Parameters("Name");

        var names = Rows().OrderBy(parameters).Select(r => r.Name);

        Assert.Equal(new[] { "Alpha", "Bravo", "Charlie" }, names);
    }

    [Fact]
    public void OrderByParametersFollowsToggledDirection()
    {
        var parameters = new Parameters("Id");
        parameters.SetSortBy("Id");

        var ids = Rows().OrderBy(parameters).Select(r => r.Id);

        Assert.Equal(new[] { 3, 2, 1 }, ids);
    }

    [Fact]
    public void OrderByUnknownColumnLeavesOrderUnchanged()
    {
        var ids = Rows().OrderBy("DoesNotExist", Direction.Ascending).Select(r => r.Id);

        Assert.Equal(new[] { 2, 3, 1 }, ids);
    }

    [Fact]
    public void OrderByNullColumnLeavesOrderUnchanged()
    {
        var ids = Rows().OrderBy(new Parameters()).Select(r => r.Id);

        Assert.Equal(new[] { 2, 3, 1 }, ids);
    }
}
