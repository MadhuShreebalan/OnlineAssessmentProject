
using System.Web.Mvc;

namespace OnlineAssessmentApplication.Filters
{
    public sealed class TeacherAuthorizeFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.User.IsInRole("Teacher"))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "dashboard", Action = "index" }));
            }
        }
    }
}