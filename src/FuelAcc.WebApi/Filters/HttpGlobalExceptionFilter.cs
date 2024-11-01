using FuelAcc.Application.UseCases.Commons.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net;

namespace FuelAcc.WebApi.Filters
{
    public sealed class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            var details = CreateProblemDetails(context);

            context.Result = new ObjectResult(details) { StatusCode = details.Status };
            context.HttpContext.Response.StatusCode = details.Status.Value;
            context.ExceptionHandled = true;
        }

        private ProblemDetails CreateProblemDetails(ExceptionContext context)
        {
            ProblemDetails problemDetails;
            var exception = context.Exception;
            if (exception.GetType() == typeof(DomainException))
            {
                problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                    Detail = exception.Message
                };
            }
            else if (exception.GetType() == typeof(UnauthorizedAccessException))
            {
                problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status403Forbidden,
                    Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status403Forbidden),
                };
            }
            else if (exception.GetType() == typeof(NotFoundException))
            {
                problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                    Detail = exception.Message
                };
            }
            else
            {
                problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = ReasonPhrases.GetReasonPhrase(StatusCodes.Status500InternalServerError),
                };
            }

            if (!_env.IsDevelopment())
            {
                return problemDetails;
            }

            problemDetails.Detail = exception.ToString();
            //problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;

            return problemDetails;
        }
    }
}
