using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace TeamCityNotifier.NotificationServer
{
    public class BuildsConnection : PersistentConnection
    {
        protected override async Task OnReceived(IRequest request, string connectionId, string data)
        {
            await base.OnReceived(request, connectionId, data);
        }
    }
}