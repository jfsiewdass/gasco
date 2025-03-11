

using Microsoft.AspNetCore.Mvc;

namespace Gasco.Common.ServerResponses
{
    public class JSendResponse : JSendResponse<object> { }
    public class JSendResponse<T>
    {
        public string Status { get; set; } = ResponseStatus.SUCCESS;
        public T? Data { get; set; }
        public int Code { get; set; } = 200;
        public string? Message { get; set; }

        public OkObjectResult OkResponse() => new OkObjectResult(this);

        public override string ToString()
        {
            return $"Status: {Status}, Data: {Data}, Code: {Code}, Message: {Message}";
        }
    }

    public static class ResponseStatus
    {
        public static string SUCCESS = "Success";
        public static string FAIL = "Error";
    }
}
