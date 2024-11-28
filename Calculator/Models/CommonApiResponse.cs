namespace Calculator.Models;

public class CommonApiResponse<T>
{
    // Indicates if the request was successful
    public bool IsSuccess { get; set; }

    // The data that is returned from the API (could be a list, object, etc.)
    public T? Data { get; set; }

    // An optional error message or details if the request failed
    public string? ErrMsg { get; set; }

    // Message that explains the status of the response (success or failure)
    public string? Message { get; set; }

    // HTTP Status code to be sent back to the client
    public int StatusCode { get; set; }

    public static CommonApiResponse<T> Success(T data, string? message = null, int statusCode = 200)
    {
        return new CommonApiResponse<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static CommonApiResponse<T> Error(string errMsg, int statusCode = 400)
    {
        return new CommonApiResponse<T>
        {
            IsSuccess = false,
            ErrMsg = errMsg,
            StatusCode = statusCode
        };
    }
}