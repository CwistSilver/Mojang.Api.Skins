using Microsoft.Extensions.Http;

namespace Mojang.Api.Skins.Utilities;
public class DefaultHttpClientFactory : IHttpClientFactory
{
    private static readonly HttpClient _httpClient;

    static DefaultHttpClientFactory()
    {
        var handler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(1) };
        var policyHandler = new PolicyHttpMessageHandler(HttpClientExtension.GetRetryPolicy()) { InnerHandler = handler };

        _httpClient = new HttpClient(policyHandler);
        _httpClient.AddSignature();
    }

    public HttpClient CreateClient(string name)
    {
        if (nameof(Skins) != name)
            throw new Exception($"This HttpClientFactory only contains '{nameof(Skins)}'.");

        return _httpClient;
    }
}
