using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;

namespace RitterToDo.Tests.TestHelpers
{
    public static class CollectionLambdaAssert
    {
        public static void ShouldNotContain<T>(this IEnumerable<T> list, Func<T, bool> checkFunc)
        {
            foreach (var i in list)
            {
                checkFunc(i).ShouldBeFalse();
            }
        }
    }
}
