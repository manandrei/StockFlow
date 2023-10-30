namespace StockFlow.ResultPattern;

public sealed class ResultPaged<T> : Result<T>, IResultPaged<T> where T : class
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; } = 10;

    public static IResultPaged<T> Success(T data, int currentPage, int pageCount, int pageSize)
    {
        return new ResultPaged<T>
        {
            Data = data,
            CurrentPage = currentPage,
            PageCount = pageCount,
            PageSize = pageSize,
        };
    }

    public static IResultPaged<T> Failure(string error, int currentPage, int pageCount, int pageSize)
    {
        return new ResultPaged<T>
        {
            CurrentPage = currentPage,
            PageCount = pageCount,
            PageSize = pageSize,
            Errors = new List<string> { error }
        };
    }

    public static IResultPaged<T> Failure(IEnumerable<string> errors, int currentPage, int pageCount, int pageSize)
    {
        return new ResultPaged<T>
        {
            CurrentPage = currentPage,
            PageCount = pageCount,
            PageSize = pageSize,
            Errors = errors.ToList()
        };
    }
}