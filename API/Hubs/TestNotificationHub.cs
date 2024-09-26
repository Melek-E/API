using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class TestNotificationHub : Hub
    {
        public async Task SendTestNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveTestNotification", message);
        }
    }
}
