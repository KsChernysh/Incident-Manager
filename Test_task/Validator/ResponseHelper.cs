using Microsoft.AspNetCore.Mvc;

namespace Test_task.Validator
{
    public class ResponseHelper
    {
        public static IActionResult CreateErrorResponse(string error, string message, int statusCode)
        {
            return new BadRequestObjectResult(new
            {
                Error = error,
                Message = message,
                StatusCode = statusCode
            });
        }
    }
}
