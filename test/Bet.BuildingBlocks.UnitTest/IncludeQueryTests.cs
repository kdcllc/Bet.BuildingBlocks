using Bet.BuildingBlocks.Domain.Abstractions.Extensions;
using Bet.BuildingBlocks.UnitTest.Models;

namespace Bet.BuildingBlocks.UnitTest;

public class IncludeQueryTests
{
    private IncludeQueryBuilder _includeQueryBuilder = new IncludeQueryBuilder();

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeSimpleType_With_Include()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();

        // There may be ORM libraries where including a simple type makes sense.
        var newIncludeQuery = includeQuery.Include(b => b.Title);
        Assert.Contains(newIncludeQuery.Paths, path => path == nameof(Book.Title));
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeFunction_With_Include()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();

        // This include does not make much sense, but it should at least do not modify paths.
        var newIncludeQuery = includeQuery.Include(b => b.GetNumberOfSales());

        // The resulting paths should not include number of sales.
        Assert.DoesNotContain(newIncludeQuery.Paths, path => path == nameof(Book.GetNumberOfSales));
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeObject_With_Include()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var newIncludeQuery = includeQuery.Include(b => b.Author);
        Assert.Contains(newIncludeQuery.Paths, path => path == nameof(Book.Author));
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeCollection_With_Include()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var newIncludeQuery = includeQuery.Include(b => b.Author.Friends);
        var expectedPath = $"{nameof(Book.Author)}.{nameof(Person.Friends)}";
        Assert.Contains(newIncludeQuery.Paths, path => path == expectedPath);
    }

    [Fact]
    public void Should_IncreaseNumberOfPathsByOne_With_Include()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var numberOfPathsBeforeInclude = includeQuery.Paths.Count;
        var newIncludeQuery = includeQuery.Include(b => b.Author.Friends);
        var numberOfPathsAferInclude = newIncludeQuery.Paths.Count;
        var expectedNumerOfPaths = numberOfPathsBeforeInclude + 1;
        Assert.Equal(expectedNumerOfPaths, numberOfPathsAferInclude);
    }

    [Fact]
    public void Should_NotModifyAnotherPath_With_Include()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var pathsBeforeInclude = includeQuery.Paths;
        var newIncludeQuery = includeQuery.Include(b => b.Author.Friends);
        var pathsAfterInclude = newIncludeQuery.Paths;
        Assert.Subset(pathsAfterInclude, pathsBeforeInclude);
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeSimpleType_With_ThenInclude()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var pathBeforeInclude = includeQuery.Paths.First();

        // There may be ORM libraries where including a simple type makes sense.
        var newIncludeQuery = includeQuery.ThenInclude(p => p.Age);
        var pathAfterInclude = newIncludeQuery.Paths.First();
        var expectedPath = $"{pathBeforeInclude}.{nameof(Person.Age)}";

        Assert.Equal(expectedPath, pathAfterInclude);
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeFunction_With_ThenInclude()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var pathBeforeInclude = includeQuery.Paths.First();

        // This include does not make much sense, but it should at least not modify the paths.
        var newIncludeQuery = includeQuery.ThenInclude(p => p.GetQuote());
        var pathAfterInclude = newIncludeQuery.Paths.First();

        Assert.Equal(pathBeforeInclude, pathAfterInclude);
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeObject_With_ThenInclude()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var pathBeforeInclude = includeQuery.Paths.First();

        var newIncludeQuery = includeQuery.ThenInclude(p => p.FavouriteBook);
        var pathAfterInclude = newIncludeQuery.Paths.First();
        var expectedPath = $"{pathBeforeInclude}.{nameof(Person.FavouriteBook)}";

        Assert.Equal(expectedPath, pathAfterInclude);
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludeCollection_With_ThenInclude()
    {
        var includeQuery = _includeQueryBuilder.WithObjectAsPreviousProperty();
        var pathBeforeInclude = includeQuery.Paths.First();

        var newIncludeQuery = includeQuery.ThenInclude(p => p.Friends);
        var pathAfterInclude = newIncludeQuery.Paths.First();
        var expectedPath = $"{pathBeforeInclude}.{nameof(Person.Friends)}";

        Assert.Equal(expectedPath, pathAfterInclude);
    }

    [Fact]
    public void Should_ReturnIncludeQueryWithCorrectPath_IfIncludePropertyOverCollection_With_ThenInclude()
    {
        var includeQuery = _includeQueryBuilder.WithCollectionAsPreviousProperty();
        var pathBeforeInclude = includeQuery.Paths.First();

        var newIncludeQuery = includeQuery.ThenInclude(p => p.FavouriteBook);
        var pathAfterInclude = newIncludeQuery.Paths.First();
        var expectedPath = $"{pathBeforeInclude}.{nameof(Person.FavouriteBook)}";

        Assert.Equal(expectedPath, pathAfterInclude);
    }
}
