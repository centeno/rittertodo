using System;
namespace RitterToDo.Core
{
    public interface IIdentityHelper
    {
        string GetUserId(System.Security.Principal.IPrincipal userPrincipal);
    }
}
