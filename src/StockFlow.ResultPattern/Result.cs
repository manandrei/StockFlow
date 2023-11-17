namespace StockFlow.ResultPattern;

public class Result<T> : IResult<T> where T : class?
{
    public T? Data { get; init; }
    public List<string> Errors { get; set; } = new();
    public string ErrorMessage => string.Join(", ", Errors);
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => !IsSuccess;

    protected Result()
    {
    }

    private Result(T? data)
    {
        Data = data;
    }

    private Result(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);
    }

    private Result(string error)
    {
        Errors.Add(error);
    }

    public static IResult<T> Success()
    {
        return new Result<T>();
    }

    public static IResult<T> Success(T data)
    {
        return new Result<T>(data);
    }

    public static IResult<T> Failure()
    {
        return new Result<T>();
    }

    public static IResult<T> Failure(string error)
    {
        return new Result<T>(error);
    }

    public static IResult<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T>(errors);
    }
}

public sealed class Result : IResult
{
    public List<string> Errors { get; set; } = new();
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => !IsSuccess;
    public string ErrorMessage { get; } = null!;

    private Result()
    {
    }

    private Result(string errorMessage)
    {
        Errors.Add(errorMessage);
    }
    
    private Result(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);
    }

    public static IResult Success()
    {
        return new Result();
    }

    public static IResult Failure(string error)
    {
        return new Result(error);
    }
    
    public static IResult Failure(IEnumerable<string> errors)
    {
        return new Result(errors);
    }
}