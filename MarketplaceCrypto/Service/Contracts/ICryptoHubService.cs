using Microsoft.AspNetCore.SignalR;

namespace Service.Contracts;

public interface ICryptoHubService
{
    Task UpdateAllCrypto(object message);

}