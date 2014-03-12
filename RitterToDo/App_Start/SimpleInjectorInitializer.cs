[assembly: WebActivator.PostApplicationStartMethod(typeof(RitterToDo.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace RitterToDo.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RitterToDo.Models;
    using Microsoft.Owin.Security;
    using System.Web;
    using System.Security.Principal;
    using System.Threading;
    using RitterToDo.Repos;
    using RitterToDo.Core;
    using Moo;
    using System;
    using System.Linq.Expressions;
    using SimpleInjector.Advanced;
    using System.Collections.Generic;
    using System.Linq;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<UserManager<ApplicationUser>>(
                () => (new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))));

            container.RegisterPerWebRequest<IPrincipal>(
                () => Thread.CurrentPrincipal);

            container.Register<IIdentityHelper, IdentityHelper>();

            container.Register<IRepository<ToDo>, ToDoRepository>();

            container.ResolveUnregisteredType += (s, e) =>
            {
                Type type = e.UnregisteredServiceType;
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == typeof(IMapper<,>))
                {
                    var args = type.GetGenericArguments();

                    e.Register(() => MappingRepository.Default.ResolveMapper(args[0], args[1]));
                }
            };
        }
    }

}