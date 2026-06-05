using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FutureStage2026.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var AdminId = context.HttpContext.Session.GetString("AdminId");

            if (string.IsNullOrEmpty(AdminId))
            {
                context.Result = new RedirectToActionResult("Login", "Admin", new { area=""});
            }

            base.OnActionExecuting(context);
        }
    }
}
