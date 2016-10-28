using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReservationSystem.Web.Startup))]
namespace ReservationSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
