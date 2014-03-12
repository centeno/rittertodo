using Seterlund.CodeGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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