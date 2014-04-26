using System.Web;
using Microsoft.AspNet.Identity;

namespace RitterToDo.Core
{
    public class IdentityHelper : RitterToDo.Core.IIdentityHelper
    {
        public string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }
    }
}