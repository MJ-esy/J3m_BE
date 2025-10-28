using System.Net;
using System.Text.Json;
using J3m_BE.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace J3m_BE.Middleware;

// Middleware for centralized error handling

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
        => _next = next;

    // Middleware invocation
    public async Task InvokeAsync(HttpContext context)
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

    // Maps exceptions to HTTP status codes and constructs ProblemDetails response
    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Determine status code and title based on exception type
        var (statusCode, title) = ex switch
        {
            NotFoundDomainException => (HttpStatusCode.NotFound, "Not Found"),
            ConflictDomainException => (HttpStatusCode.Conflict, "Conflict"),
            DomainException => (HttpStatusCode.BadRequest, "Validation Error"),
            _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
        };

        // Create ProblemDetails response
        var problem = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        // Write ProblemDetails to response
        context.Response.StatusCode = problem.Status.Value;
        context.Response.ContentType = "application/problem+json";

        // Use camelCase for JSON properties
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Serialize and write the problem details
        await context.Response.WriteAsync(JsonSerializer.Serialize(problem, options));
    }
}