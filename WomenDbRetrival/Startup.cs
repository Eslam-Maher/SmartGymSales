using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WomenDbRetrival.Startup))]
namespace WomenDbRetrival
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
