using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_akka.Startup))]
namespace mvc_akka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
