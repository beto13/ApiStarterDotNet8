using System.Net;

namespace Application.Common
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public T Data { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message, bool success, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Success = success;
            Data = data;
        }
    }
}
