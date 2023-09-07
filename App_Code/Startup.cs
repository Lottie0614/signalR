using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(SignalRChat_Practice.App_Code.Startup))]
namespace SignalRChat_Practice.App_Code
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}