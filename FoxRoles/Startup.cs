using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoxRoles.Startup))]
namespace FoxRoles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
