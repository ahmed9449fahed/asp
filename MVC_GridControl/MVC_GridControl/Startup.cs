using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_GridControl.Startup))]
namespace MVC_GridControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
