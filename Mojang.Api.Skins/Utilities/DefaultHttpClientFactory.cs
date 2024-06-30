using Microsoft.Extensions.Http;

namespace Mojang.Api.Skins.Utilities;
public class DefaultHttpClientFactory : IHttpClientFactory
{
    private static readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    private PolicyHttpMessageHandler? _cachedHttpMessageHandler;

    private HttpMessageHandler CreateHandler()
    {
        if (_cachedHttpMessageHandler is not null)
            return _cachedHttpMessageHandler;

        _semaphoreSlim.Wait();
        try
        {
            if (_cachedHttpMessageHandler is not null)
                return _cachedHttpMessageHandler;

            var httpClientHandler = new HttpClientHandler();
            _cachedHttpMessageHandler = new PolicyHttpMessageHandler(HttpClientExtension.GetRetryPolicy())
            {
                InnerHandler = httpClientHandler
            };
        }
        finally
        {
            _semaphoreSlim.Release();
        }

        return _cachedHttpMessageHandler;
    }

    public HttpClient CreateClient(string name)
    {
        if (nameof(Skins) != name)
            throw new Exception($"This HttpClientFactory only contains '{nameof(Skins)}'.");

        var handler = CreateHandler();
        var httpClient = new HttpClient(handler, disposeHandler: false);
        httpClient.AddSignature();

        return httpClient;
    }
}
