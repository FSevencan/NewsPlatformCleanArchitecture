using Core.Security.Extensions;
using Humanizer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Middleware;
public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RateLimitingService _rateLimitingService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RateLimitingMiddleware(RequestDelegate next, RateLimitingService rateLimitingService, IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _rateLimitingService = rateLimitingService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        
        if (context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
        {
            int? userId = _httpContextAccessor.HttpContext?.User.GetUserId();

            if (userId.HasValue)
            {
                var entityType = GetEntityTypeFromRequest(context.Request);

                if (!string.IsNullOrEmpty(entityType))
                {
                    var isLimitExceeded = await _rateLimitingService.IsRateLimitExceeded(entityType, userId.Value.ToString());
                    if (isLimitExceeded)
                    {
                        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                        context.Response.ContentType = "application/json";
                        var errorResponse = JsonSerializer.Serialize(new { error = "Çok fazla istek gönderdiniz. Lütfen bir dakika sonra tekrar deneyiniz." });
                        await context.Response.WriteAsync(errorResponse);
                        return;
                    }
                }
            }
        }

        await _next(context);
    }

    private string GetEntityTypeFromRequest(HttpRequest request)
    {
        var pathSegments = request.Path.Value?.Trim('/').Split('/');

        if (pathSegments != null && pathSegments.Length > 1)
        {
            var controllerName = pathSegments[1];
            if (controllerName.EndsWith("controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName[..^"controller".Length];
            }

            // Çoğul isimleri tekilleştir
            controllerName = controllerName.Singularize(false);

            return controllerName;
        }

        return string.Empty;
    }
}

