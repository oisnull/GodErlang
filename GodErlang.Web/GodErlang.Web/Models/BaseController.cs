using GodErlang.Entity.Extends;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GodErlang.Web.Models
{
    public class BaseController : Controller
    {
        protected SessionUser CurrentUser { get; private set; }

        public BaseController()
        {
        }

        public virtual bool IsCheckUser()
        {
            return true;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.IsCheckUser())
            {
                this.CurrentUser = UserSessionManager.GetCurrentUser();
                if (CurrentUser == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new ContentResult()
                        {
                            ContentType = "text/html",
                            Content = "<script>TimeoutError();</script>"
                        };
                    }
                    else
                    {
                        filterContext.Result = RedirectToRoute("Default", new { Controller = "account", Action = "signin" });
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}