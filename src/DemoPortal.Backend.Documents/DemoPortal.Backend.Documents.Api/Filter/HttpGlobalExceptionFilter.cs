using System.Net;
using DemoPortal.Backend.Documents.Abstractions.Errors;
using DemoPortal.Backend.Shared.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoPortal.Backend.Documents.Api.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public HttpGlobalExceptionFilter(
            ILogger<HttpGlobalExceptionFilter> logger, 
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            BusinessResult businessResult;
            if (_webHostEnvironment.IsDevelopment())
            {
                businessResult = new ErrorModel(DocumentsErrorModelKeys.Exception, context.Exception.Message);
            }
            else
            {
                businessResult = DocumentsErrorModels.CommonError;
            }

            context.Result = new ObjectResult(businessResult);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Accepted;
            context.ExceptionHandled = true;
        }
    }
}