using RitterToDo.Core;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitterToDo.IntegratedTests.TestHelpers
{
    public static class TestContainer
    {
        static TestContainer()
        {
            innerContainer = new Container();
            RitterToDo.App_Start.SimpleInjectorInitializer.InitializeContainer(innerContainer);
            innerContainer.Register<IIdentityHelper, TestIdentityHelper>();
        }

        private static readonly Container innerContainer;

        public static T GetInstance<T>() where T : class
        {
            return innerContainer.GetInstance<T>();
        }
    }
}
