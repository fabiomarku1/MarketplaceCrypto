using Microsoft.AspNetCore.SignalR;
using Service.Contracts;
using Shared.Utility;

namespace Service.Services;

public class CryptoHubService:ICryptoHubService
{
    private readonly IHubContext<SignalHub> _hub;
    public CryptoHubService( IHubContext<SignalHub> hub)
    {
        _hub = hub;
    }
    public async void UpdateAllCrypto( object message)
    {
        await _hub.Clients.All.SendAsync("UpdateCryptocurrencies", message);
    }
}
