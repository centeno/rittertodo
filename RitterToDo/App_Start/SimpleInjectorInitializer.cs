[assembly: WebActivator.PostApplicationStartMethod(typeof(RitterToDo.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace RitterToDo.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RitterToDo.Models;
    using System.Threading;
    using RitterToDo.Repos;
    using RitterToDo.Core;
    using Moo;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC5 Dependency Resolver.</summary>
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
            container.Register(
                () => (new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()))));

            container.RegisterPerWebRequest(
                () => Thread.CurrentPrincipal);

            container.Register<IIdentityHelper, IdentityHelper>();

            container.Register<IRepository<ToDo>, ToDoRepository>();

            container.RegisterOpenGeneric(typeof(IRepository<>), typeof(BaseRepository<>));
            
            container.Register<IApplicationDbContext, ApplicationDbContext>();

            container.RegisterSingle(MappingRepository.Default);
        }
    }

}