using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WholesaleTradingPortal.Startup))]
namespace WholesaleTradingPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
