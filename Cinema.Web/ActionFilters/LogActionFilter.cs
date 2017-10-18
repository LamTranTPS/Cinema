using System;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Routing;

namespace Cinema.Web.ActionFilters
{
    public class AdminLog : ActionFilterAttribute

    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Log("OnActionExecuting", actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            Log("OnActionExecuted", actionExecutedContext.ActionContext);
        }

        private void Log(string methodName, HttpActionContext context)
        {
            var controllerName = context.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = context.ActionDescriptor.ActionName;
            var uri = context.Request.RequestUri.LocalPath;
            var message = String.Format("{0} controller:{1} action:{2}, urri : {3}", methodName, controllerName, actionName, uri);
            Debug.WriteLine(message, "Admin Log");
        }
    }
}