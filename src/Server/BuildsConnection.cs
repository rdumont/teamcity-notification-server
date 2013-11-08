using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace TeamCityNotifier.NotificationServer
{
    public class BuildsConnection : PersistentConnection
    {
        protected override async Task OnReceived(IRequest request, string connectionId, string buildTypeId)
        {
            await this.Groups.Add(connectionId, buildTypeId);
            await base.OnReceived(request, connectionId, buildTypeId);
        }
    }
}