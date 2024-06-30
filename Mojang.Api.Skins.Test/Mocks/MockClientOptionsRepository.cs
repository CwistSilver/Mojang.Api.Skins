using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Repository.Options;

namespace Mojang.Api.Skins.Test.Mocks;
public class MockClientOptionsRepository : IClientOptionsRepository
{
    public ClientOptions Options { get; set; } = ClientOptions.Default;

    public ClientOptions GetOptions() => Options;

    public Task<ClientOptions> GetOptionsAsync() => Task.FromResult(Options);


    public Task SetOptionsAsync(ClientOptions options)
    {
        Options = options;
        return Task.CompletedTask;
    }
}
