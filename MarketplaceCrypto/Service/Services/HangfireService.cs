using Service.Contracts;

namespace Service.Services;

public class HangfireService : IHangfireService
{
    private readonly IBinanceService _binanceService;

    public HangfireService(IBinanceService binanceService)
    {
        _binanceService = binanceService;
    }

    public Task UpdateDataMarket()
    {
        Task.Run(async () =>
            {
                await _binanceService.UpdateValues();
            });
        return Task.CompletedTask;
    }


}