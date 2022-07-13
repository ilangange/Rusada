using System.Collections.Generic;

namespace Rusada.Aviation.API.Extensions
{
    public class ApiResponse
    {
        public static object GenerateResponse(bool isSuccess, object data, List<string> errors)
        {
            return new
            {
                success = isSuccess,
                data = data,
                errors = errors
            };
        }
    }
}
