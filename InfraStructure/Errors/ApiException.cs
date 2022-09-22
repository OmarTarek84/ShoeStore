
namespace InfraStructure.Errors
{
    public class ApiException
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;

        public ApiException(int status, string? message = null)
        {
            Status = status;
            Message = message ?? GetStatusMessage(status);
        }

        private string GetStatusMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Not Authorized",
                404 => "404 Not Found",
                403 => "Forbidden",
                500 => "Internal Server Error",
                _ => "Unknown Error, please try again later"
            };
        }
    }
}
