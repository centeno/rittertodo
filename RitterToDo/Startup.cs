using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RitterToDo.Startup))]
namespace RitterToDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
