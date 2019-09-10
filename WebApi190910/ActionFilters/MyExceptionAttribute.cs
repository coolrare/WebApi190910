using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using WebApi190910.Models;

namespace WebApi190910.ActionFilters
{
    public class MyExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ArgumentException)
            {
                // TODO
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new MyHttpError()
            {
                ErrorCode = "100",
                ErrorMsg = "發生例外: " + actionExecutedContext.Exception.Message
            });
        }
    }
}