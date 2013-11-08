using Microsoft.Owin.Cors;
using Owin;

namespace TeamCityNotifier.NotificationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR<BuildsConnection>("/builds");
        }
    }
}