namespace RitterToDo.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RitterToDo.Core;
    using RitterToDo.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "guest";
            string password = "password";

            var user = new ApplicationUser();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);

            var generator = new DummyDataGenerator();
            context.ToDos.AddRange(generator.CreateDummyToDos(user));
            context.SaveChanges();

            base.Seed(context);
        }

    }
}
