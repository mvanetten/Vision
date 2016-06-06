using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vision.WebUI.Startup))]
namespace Vision.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
