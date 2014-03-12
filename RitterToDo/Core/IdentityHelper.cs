﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

namespace RitterToDo.Core
{
    public class IdentityHelper : RitterToDo.Core.IIdentityHelper
    {
        public string GetUserId(IPrincipal userPrincipal)
        {
            return userPrincipal.Identity.GetUserId();
        }
    }
}