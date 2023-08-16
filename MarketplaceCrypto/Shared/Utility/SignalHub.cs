using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Shared.Utility;

[Authorize]
public class SignalHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public async Task UpdateCryptocurrencies(object message)
    {
        await Clients.All.SendAsync("UpdateMarket", message);
    }


    public async Task GetInitialData()
    {
        await Clients.All.SendAsync("UpdateMarket", null);
    }
    public static string UserGroupName(int userId)
    {
        return $"User_{userId}";
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}