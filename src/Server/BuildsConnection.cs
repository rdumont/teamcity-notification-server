using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace TeamCityNotifier.NotificationServer
{
    public class BuildsConnection : PersistentConnection
    {
        protected override async Task OnReceived(IRequest request, string connectionId, string buildTypeIds)
        {
            var ids = buildTypeIds.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(id => id.Trim());
            
            foreach (var id in ids)
                await this.AddToGroups(connectionId, id);

            await base.OnReceived(request, connectionId, buildTypeIds);
        }

        protected virtual Task AddToGroups(string connectionId, string buildTypeId)
        {
            Console.WriteLine("Connection {0} registered to build {1}", connectionId.Substring(0, 8), buildTypeId);
            return this.Groups.Add(connectionId, buildTypeId);
        }
    }
}