using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins.Repository.Options;
public class ClientOptionsRepository : IClientOptionsRepository
{
    private ClientOptions _options = ClientOptions.Default;
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public ClientOptions GetOptions()
    {
        _semaphoreSlim.Wait();
        try
        {
            return _options;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task<ClientOptions> GetOptionsAsync()
    {
        await _semaphoreSlim.WaitAsync();
        try
        {
            return _options;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task SetOptionsAsync(ClientOptions options)
    {
        await _semaphoreSlim.WaitAsync();
        try
        {
            _options = options;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}
