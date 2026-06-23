namespace WebApplication1.Entities.Commons
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Ok") => new ApiResponse<T>
        {
            Message = message,
            Success = true,
            Data = data
        };

        public static ApiResponse<T> FailResponse(string message) => new ApiResponse<T> { Success = false, Data = default, Message = message };
    }
}
