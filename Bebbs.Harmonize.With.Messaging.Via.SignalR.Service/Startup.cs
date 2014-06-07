using Microsoft.Owin.Cors;
using Owin;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Service
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
