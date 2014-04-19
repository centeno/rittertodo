using Seterlund.CodeGuard;
using System.Security.Principal;
using System.Web.Mvc;

namespace RitterToDo.App_Start
{
    public class IPrincipalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Guard.That(() => controllerContext).IsNotNull();
            Guard.That(() => bindingContext).IsNotNull();

            IPrincipal p = controllerContext.HttpContext.User;
            return p;
        }
    }
}