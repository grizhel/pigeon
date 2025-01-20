using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace pigeon_lib.Utils;

public class ReactedResult<T>
{
    public bool Success { get; set; }

    public string? Message { get; set; } = null;

    public T? Model { get; set; } = default;

    public HttpStatusCode StatusCode { get; set; }

    public static ReactedResult<T> Successful(T? data = default, string message = "")
    {
        return new ReactedResult<T>
        {
            StatusCode = HttpStatusCode.OK,
            Success = true,
            Model = data,
            Message = message,
        };
    }

    public static ReactedResult<T> Failed(HttpStatusCode statusCode, string message)
    {
        return new ReactedResult<T>
        {
            StatusCode = statusCode,
            Success = false,
            Message = message,
        };
    }
}
