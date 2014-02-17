using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Elmah.Web.Startup))]
namespace Elmah.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}