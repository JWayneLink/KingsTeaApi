
using KTA.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using IActionFilter = Microsoft.AspNetCore.Mvc.Filters.IActionFilter;

namespace KingsTeaApp.Filter
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {           
            if (!context.ModelState.IsValid)
            {
                if (
                    context.ModelState.Values.FirstOrDefault() != null && 
                    context.ModelState.Values.First().Errors.FirstOrDefault() != null
                )
                {
                    //context.ModelState.Values.First().Errors.First().ErrorMessage
                    string valideErrorMsg = context.ModelState.Values.First().Errors.First().ErrorMessage;
                    JObject jObj = new JObject();
                    jObj["isSuccess"] = false;
                    jObj["message"] = valideErrorMsg;
                    jObj["data"] = string.Empty;
                    string json = JsonConvert.SerializeObject(jObj);

                    context.Result = new ContentResult()
                    {
                        StatusCode = (int)System.Net.HttpStatusCode.OK,
                        Content = json,
                        ContentType = "application/json"
                    };
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
