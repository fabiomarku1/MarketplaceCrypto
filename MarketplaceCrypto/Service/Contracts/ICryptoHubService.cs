using Microsoft.AspNetCore.SignalR;

namespace Service.Contracts;

public interface ICryptoHubService
{
    void UpdateAllCrypto(object message);

}