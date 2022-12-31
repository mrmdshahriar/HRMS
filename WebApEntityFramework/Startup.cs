using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApEntityFramework.Startup))]
namespace WebApEntityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
