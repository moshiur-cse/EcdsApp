using EcdsApp.Data;
using EcdsApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EcdsApp.Security
{
    public class UserAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
                return;

            var currentUser = filterContext.HttpContext.User.Identity.Name;
            var controllerName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)filterContext.Controller).ControllerContext.ActionDescriptor.ActionName;

            if (!CheckUserAccess(currentUser, controllerName, actionName))
                filterContext.Result = new NotFoundResult();
        }

        private static bool CheckUserAccess(string userName, string controllerName, string actionName)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(controllerName))
                return false;

            using var dbContext = new DataContext();

            var userObj = dbContext.Users.FirstOrDefault(u => u.UserName == userName);
            if (userObj == null)
                return false;

            var userPermMenuContent = dbContext.RoleWisePermittedContents
                .Include(model => model.UserPermittedContent)
                .FirstOrDefault(u =>
                    u.UserPermittedContent.ActionName == actionName &&
                    u.UserPermittedContent.ControllerName == controllerName && u.UserRoleId == userObj.UserRoleId);

            return userPermMenuContent != null || userObj.UserRoleId == AppStaticBase.SystemAdministrator;
            //return userContent != null;
        }
    }
}
