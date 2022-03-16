﻿using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using YoumaconSecurityOps.Core.Shared.Logging;
using YoumaconSecurityOps.Core.Shared.Responses;
using YoumaconSecurityOps.Web.Client.Bootstrapping;
using YoumaconSecurityOps.Web.Client.Invariants;

namespace YoumaconSecurityOps.Web.Client.Middleware;

public class ExceptionLogger
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionLogger> _logger;

    private readonly ApiExceptionOptions _options;

    public ExceptionLogger(RequestDelegate next, ILogger<ExceptionLogger> logger, ApiExceptionOptions options)
    {
        _next = next;
        _logger = logger;
        _options = options;
    }

    public async Task Invoke(HttpContext context, NavigationManager navigationManager)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _options);
            //navigationManager.NavigateTo("/appError");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, ApiExceptionOptions options)
    {
        var correlationId = Guid.NewGuid().ToString();

        var outcome = OperationOutcome.UnsuccessfulOutcome;
        outcome.CorrelationId = correlationId;
        outcome.Message = String.Format(Errors.UnhandledError, correlationId);

        options.AddResponseDetails?.Invoke(context, exception, outcome);

        var resolvedExceptionMessage = GetInnermostExceptionMessage(exception);

        var level = _options.DetermineLogLevel?.Invoke(exception) ?? LogLevel.Error;

        _logger.Log(
            level,
            EventIDs.EventIdUncaughtGlobal,
            exception,
            MessageTemplates.UncaughtGlobal,
            resolvedExceptionMessage,
            correlationId
            );

        var apiResponse = new ApiResponse<IEnumerable<String>>
        {
            ResponseCode = ResponseCodes.ApiError,
            Data = Enumerable.Empty<string>(),
            Outcome = outcome
        };

        var result = JsonSerializer.Serialize(apiResponse, Common.JsonSerializerOptions);
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(result);
        context.Response.Redirect("/appError");
    }

    private static string GetInnermostExceptionMessage(Exception exception)
    {
        var exceptionToCheck = exception;

        while (exceptionToCheck.InnerException is not null)
        {
            if (exception.InnerException is null)
            {
                break;
            }

            exceptionToCheck = exceptionToCheck.InnerException;
        }

        return exceptionToCheck.Message;
    }
}