using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Should;

namespace RitterToDo.Tests.TestHelpers
{
    public static class ActionResultExtensions
    {
        public static ViewResult ShouldBeViewResult(this ActionResult actionResult)
        {
            actionResult.ShouldBeType<ViewResult>();
            return (ViewResult)actionResult;
        }

        public static RedirectResult ShouldBeRedirectResult(this ActionResult actionResult)
        {
            actionResult.ShouldBeType<RedirectResult>();
            return (RedirectResult)actionResult;
        }
    }
}
