using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using EWallet.Common.Exceptions;
using System;

namespace EWallet.WebApp.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilterAttribute> logger;

        public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            switch (context.Exception)
            {
                case NotFoundErrorException notFound:
                    context.Result = new ViewResult
                    {
                        ViewName = "Views/Shared/NotFound.cshtml"
                    };

                    break;
                case UnauthorizedAccessException unauthorizedAccess:
                    context.Result = new ViewResult
                    {
                        ViewName = "Views/Shared/Unauthorized.cshtml"
                    };
                    break;
                case ValidationErrorException validationError:
                    foreach (var validationResult in validationError.ValidationResult.Errors)
                    {
                        context.ModelState.AddModelError(validationResult.PropertyName, validationResult.ErrorMessage);
                    }

                    var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                    var viewResult = new ViewResult
                    {
                        ViewName = $"Views/{descriptor.ControllerName}/{descriptor.ActionName}.cshtml"
                    };
                    viewResult.ViewData.Model = validationError.Model;
                    context.Result = viewResult;
                    break;
                default:
                    //context.Result = new ViewResult
                    //{
                    //    ViewName = "Views/Shared/InternalServerError.cshtml"
                    //};
                    throw context.Exception;
                    break;

            }
        }
    }
}
