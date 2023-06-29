using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webapp_api.Startup))]
namespace Webapp_api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
