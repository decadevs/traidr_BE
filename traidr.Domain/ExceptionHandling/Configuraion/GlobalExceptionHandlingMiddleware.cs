using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using traidr.Domain.ExceptionHandling.Exceptions;

namespace traidr.Domain.ExceptionHandling.Configuraion;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        string message = "";

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(AuthenticationError401))
        {
            message = ex.Message;
            status = HttpStatusCode.Unauthorized;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(AuthorizationError403))
        {
            message = ex.Message;
            status = HttpStatusCode.Forbidden;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(ConflictError409))
        {
            message = ex.Message;
            status = HttpStatusCode.Conflict;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(DatabaseUpdateError))
        {
            message = ex.Message;
            status = HttpStatusCode.ExpectationFailed;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(FileUploadErrors400))
        {
            message = ex.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(InvalidPayment400))
        {
            message = ex.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(RateLimitingRequest429))
        {
            message = ex.Message;
            status = HttpStatusCode.TooManyRequests;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(ResourceNotFound404))
        {
            message = ex.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(ServerError500))
        {
            message = ex.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(ValidationError400))
        {
            message = ex.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = ex.StackTrace;
        }
        else
        {
            message = ex.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = ex.StackTrace;
        }
        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}


