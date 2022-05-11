using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.API.Models;
using Ordering.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IRequestContextService _requestContext;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger,
            IWebHostEnvironment env,
            IRequestContextService reuqestContext,
            IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
            _env = env;
            _requestContext = reuqestContext;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            bool returnBadRequest = IsAjaxRequest(context.HttpContext.Request);

            var errorModel = GetErrorModel(context.Exception);

            if (returnBadRequest)
            {
                context.Result = new BadRequestObjectResult(errorModel);
            }
            else
            {
                context.Result = CreateActionResult(context, errorModel);
            }

            context.ExceptionHandled = true;
        }

        private GeneralErrorModel GetErrorModel(Exception exception)
        {
            bool passExceptionInfoToClient = _env.IsDevelopment();

            var result = new GeneralErrorModel
            {
                ErrorUniqueIdentifier = _requestContext.UniqueRequestId.ToString(),
            };

            _logger.LogError(exception, exception.Message);

            result.Title = "Unexpected error occurred.";
            result.UserMessage = passExceptionInfoToClient ? exception.Message : "Unexpected error occurred.";

            return result;
        }

        private bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers != null)
            {
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            }

            return false;
        }

        private ActionResult CreateActionResult(FilterContext context, GeneralErrorModel model)
        {
            var result = new ViewResult
            {
                ViewName = "~/Views/Error/General.cshtml",
                ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
            };

            result.ViewData.Model = model;

            return result;
        }
    }
}
