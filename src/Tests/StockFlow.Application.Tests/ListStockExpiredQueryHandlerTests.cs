using System.Linq.Expressions;
using Moq;
using StockFlow.Application.Stocks;
using StockFlow.Application.Stocks.Query.ListStockExpired;
using StockFlow.Domain.Stocks;
using StockFlow.ResultPattern;

namespace StockFlow.Application.Tests;

public class ListStockExpiredQueryHandlerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task returns_list_of_expired_stocks_when_valid_query_is_provided()
    {
        // Arrange
        var stockRepositoryMock = new Mock<IStockRepository>();
        var queryHandler = new ListStockExpiredQueryHandler(stockRepositoryMock.Object);
        var query = new ListStockExpiredQuery();

        var expiredStocks = new List<Stock>
        {
            new Stock { ExpireDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)) },
            new Stock { ExpireDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)) }
        };

        stockRepositoryMock.Setup(r => r.GetFilteredData(
            true,
            It.IsAny<Expression<Func<Stock, bool>>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Stock, object>>[]>()
        )).ReturnsAsync(expiredStocks);

        // Act
        IResult<IEnumerable<Stock>> result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(result.Data, Is.EqualTo(expiredStocks));
    }

    [Test]
    public async Task returns_empty_list_when_no_expired_stocks_are_found()
    {
        // Arrange
        var stockRepositoryMock = new Mock<IStockRepository>();
        var queryHandler = new ListStockExpiredQueryHandler(stockRepositoryMock.Object);
        var query = new ListStockExpiredQuery();

        stockRepositoryMock.Setup(r => r.GetFilteredData(
            true,
            It.IsAny<Expression<Func<Stock, bool>>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Stock, object>>[]>()
        )).ReturnsAsync(new List<Stock>());

        // Act
        IResult<IEnumerable<Stock>> result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(result.Data!.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task includes_material_and_position_information_for_each_stock()
    {
        // Arrange
        var stockRepositoryMock = new Mock<IStockRepository>();
        var queryHandler = new ListStockExpiredQueryHandler(stockRepositoryMock.Object);
        var query = new ListStockExpiredQuery();

        var expiredStocks = new List<Stock>
        {
            new Stock { ExpireDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)) },
            new Stock { ExpireDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)) }
        };

        stockRepositoryMock.Setup(r => r.GetFilteredData(
            true,
            It.IsAny<Expression<Func<Stock, bool>>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Stock, object>>[]>()
        )).ReturnsAsync(expiredStocks);

        // Act
        IResult<IEnumerable<Stock>> result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(result.Data, Is.EqualTo(expiredStocks));

        stockRepositoryMock.Verify(r => r.GetFilteredData(
            true,
            It.IsAny<Expression<Func<Stock, bool>>>(),
            It.IsAny<CancellationToken>(),
            It.Is<Expression<Func<Stock, object>>[]>(includes =>
                includes.Contains(s => s.Material) && includes.Contains(s => s.Position)
            )
        ), Times.Once);
    }

    [Test]
    public async Task handles_null_query_parameter_gracefully()
    {
        // Arrange
        var stockRepositoryMock = new Mock<IStockRepository>();
        var queryHandler = new ListStockExpiredQueryHandler(stockRepositoryMock.Object);

        // Act
        IResult<IEnumerable<Stock>> result = await queryHandler.Handle(null, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Data);
    }

    [Test]
    public async Task handles_empty_list_of_stocks_gracefully()
    {
        // Arrange
        var stockRepositoryMock = new Mock<IStockRepository>();
        var queryHandler = new ListStockExpiredQueryHandler(stockRepositoryMock.Object);
        var query = new ListStockExpiredQuery();

        stockRepositoryMock.Setup(r => r.GetFilteredData(
            true,
            It.IsAny<Expression<Func<Stock, bool>>>(),
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Stock, object>>[]>()
        )).ReturnsAsync(new List<Stock>());

        // Act
        IResult<IEnumerable<Stock>> result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.That(result.Data!.Count(), Is.EqualTo(0));
    }

    [Test]
    public async Task handles_null_stock_repository_gracefully()
    {
        // Arrange
        var queryHandler = new ListStockExpiredQueryHandler(null);
        var query = new ListStockExpiredQuery();

        // Act
        IResult<IEnumerable<Stock>> result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Data);
    }
}