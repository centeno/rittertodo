using RitterToDo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitterToDo.IntegratedTests.TestHelpers
{
    public class TestIdentityHelper :IIdentityHelper
    {
        public string GetUserId()
        {
            return DataHelper.DefaultUserId;
        }
    }
}
