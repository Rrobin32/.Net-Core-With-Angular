using ConfigurationUtilities.Generic;
using ConfigurationUtilities.Settings;
using ConfigurationUtilities.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<ResponseMessage> errors = new List<ResponseMessage>();
                IEnumerable<string> modelError = context.ModelState.Values.SelectMany(x => x.Errors.Select(a => a.ErrorMessage));
                foreach (string error in modelError)
                {
                    Responses.ValidationError(errors, Convert.ToInt32(error.Split("~")[0]), error.Split("~")[1]);
                }
                context.Result = new ContentResult
                {
                    Content = Responses.Response(errors),
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }
            base.OnActionExecuting(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!Validators.Equals(context?.HttpContext.Request.ContentType, "application/xml"))
            {
                if (context?.Result != null)
                {
                    context.Result = new ContentResult
                    {
                        Content = ((OkObjectResult)context?.Result).Value.ToString(),
                        ContentType = context.HttpContext.Request.ContentType,
                        StatusCode = 200
                    };
                }
            }
            base.OnActionExecuted(context);
        }
    }
}

