
using Authenticador.WebApi.ActionResults;
using HP.Authenticador.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Diagnostics.CodeAnalysis;

namespace Authenticador.WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException exception)
            {
                logger.LogInformation(
                    context.Exception,
                    "Ocorre um erro de negócio: Erros: {@Erros}",
                   exception.Errors.ToDictionary(x => x.Key, x => x.Message));

                var status = StatusCodes.Status400BadRequest;

                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = status,
                    Detail = "Consulte a propriedade de erros para obter detalhes adicionais.",
                    Type = "Cliente"
                };

                exception.Errors.ToList().ForEach(error =>
                problemDetails.Errors.Add(error.Key, new string[] { error.Message }));

                context.HttpContext.Response.StatusCode = status;
                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else
            {
                logger.LogError(
                    new EventId(context.Exception.HResult),
                    context.Exception,
                    context.Exception.Message);

                var json = new JsonErrorResponse
                {
                    Messages = new[] { "Ocorreu um erro", context.Exception?.Message }
                };

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            context.ExceptionHandled = true;
        }

        private sealed class JsonErrorResponse
        {
            public string[] Messages { get; set; }
        }

    }
}
