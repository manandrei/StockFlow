namespace StockFlow.ResultPattern;

public interface IResultPaged<out T> : IResult<T> where T : class?
{
    int CurrentPage { get; set; }
    int PageCount { get; set; }
    int PageSize { get; set; }
}