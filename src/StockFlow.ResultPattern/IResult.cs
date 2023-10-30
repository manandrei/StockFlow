namespace StockFlow.ResultPattern;

public interface IResult
{
    public List<string> Errors { get; }
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public string ErrorMessage { get; }
}

public interface IResult<out T> : IResult where T : class?
{
    T? Data { get; }
}