
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
//using System.Net.Http;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;
//using System.Web.Http.ModelBinding;

//using System.Net;
//using System.Net.Http;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;

namespace KingsTeaApp.Filter
{
    //public class ValidateModelAttribute : ActionFilterAttribute
    //{
    //    //public override void OnActionExecuting(HttpActionContext actionContext)
    //    //{
    //    //    if (actionContext.ModelState.IsValid == false)
    //    //    {
    //    //        actionContext.Response = actionContext.Request.CreateErrorResponse(
    //    //            HttpStatusCode.BadRequest, actionContext.ModelState);
    //    //    }
    //    //}
    //}

    //public class ValidateModelAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        var modelState = actionContext.ModelState;

    //        if (!modelState.IsValid)
    //            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.UnprocessableEntity, modelState);
    //    }
    //}

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.SelectMany(x => x.Value.Errors).Any(x => !string.IsNullOrEmpty(x.ErrorMessage)))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }

            base.OnActionExecuting(actionContext);
        }
    }

    //public class ResultAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    //    {
    //        if (actionExecutedContext.Exception != null)
    //        {
    //            return;
    //        }

    //        var ignoreResult1 = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<IgnoreResultAttribute>().FirstOrDefault();
    //        var ignoreResult2 = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<IgnoreResultAttribute>().FirstOrDefault();
    //        if (ignoreResult1 != null || ignoreResult2 != null)
    //        {
    //            return;
    //        }

    //        var objectContent = actionExecutedContext.Response.Content as ObjectContent;

    //        var data = objectContent?.Value;

    //        var result = new ResultViewModel
    //        {
    //            success = true,
    //            data = data
    //        };

    //        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result);
    //    }
    //}

    //public class ValidateModelAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        if (!context.ModelState.IsValid)
    //        {
    //            context.Result = new BadRequestObjectResult(context.ModelState);
    //        }
    //    }
    //}
}
