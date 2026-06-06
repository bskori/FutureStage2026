using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FutureStage2026.Filters
{
    public class SchoolAuthorizeAttribute :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var SchoolId = context.HttpContext.Session.GetString("SchoolId");

            if (string.IsNullOrEmpty(SchoolId))
            {
                context.Result = new RedirectToActionResult("Login", "School", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
    }
}
