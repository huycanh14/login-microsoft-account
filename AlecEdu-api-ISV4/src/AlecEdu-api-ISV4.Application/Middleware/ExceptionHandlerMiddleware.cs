using System.Net;
using AlecEdu_api.Application.Contracts.Infrastructure;
using AlecEdu_api.Application.Exceptions;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AlecEdu_api.Application.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger
        )
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, ITokenManagerService tokenManager)
    {
        try
        {
            if (await tokenManager.IsCurrentActiveTokenOrNull())
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            }
            
            switch (context.Response.StatusCode)
            {
                case (int)HttpStatusCode.Forbidden: 
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new BaseResponse<string>
                    {
                        Success = false,
                        ResultCode = EResultCode.FORBIDDEN,
                        Message = "You don't have permission to access / on this server"
                    });
                    break;
                case (int)HttpStatusCode.Unauthorized:
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new BaseResponse<string>
                    {
                        Success = false,
                        ResultCode = EResultCode.UNAUTHORIZED,
                        Message = "Access is denied due to invalid credentials"
                    });
                    break;
            }
        }
        catch (Exception ex)
        {
            await ConvertException(context, ex);
        }
    }

    private Task ConvertException(HttpContext context, Exception exception)
    {
        int httpStatusCode;
        var result = exception.Message;
        var response = new BaseResponse<string>
        {
            Success = true
        };

        switch (exception)
        {
            case ValidationException validationException:
                httpStatusCode = (int)HttpStatusCode.BadRequest;
                response.ResultCode = EResultCode.BADREQUEST;
                response.Message = exception.Message;
                response.ValidationErrors = validationException.ValidationErrors;
                break;
            case BadRequestException badRequestException:
                httpStatusCode = (int)HttpStatusCode.BadRequest;
                response.ResultCode = EResultCode.BADREQUEST;
                response.Message = badRequestException.Message;
                break;
            case NotFoundException _:
                httpStatusCode = (int)HttpStatusCode.NotFound;
                response.ResultCode = EResultCode.NOTFOUND;
                response.Message = exception.Message;
                break;
            case ApiException apiException:
                httpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResultCode = EResultCode.INTERNALSERVERERROR;
                response.Message = apiException.Message;
                break;
            default:
                httpStatusCode = (int)HttpStatusCode.InternalServerError;
                response.ResultCode = EResultCode.INTERNALSERVERERROR;
                response.Message = exception.Message;
                break;
        }


        _logger.LogError(result);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = httpStatusCode;

        if (result == string.Empty)
        {
            response.Message = exception.Message;
        }

        JsonConvert.DefaultSettings = () =>
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                Formatting = Formatting.None
            };

            return settings;
        };
        result = JsonConvert.SerializeObject(response);

        return context.Response.WriteAsync(result);
    }
}
