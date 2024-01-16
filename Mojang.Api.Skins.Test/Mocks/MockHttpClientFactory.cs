namespace Mojang.Api.Skins.Test.Mocks;
public class MockHttpClientFactory : IHttpClientFactory
{
    private readonly HttpClient _mockHttpClient;

    public MockHttpClientFactory(HttpMessageHandler httpMessageHandler) => _mockHttpClient = new HttpClient(httpMessageHandler);
    public HttpClient CreateClient(string name) => _mockHttpClient;
}
