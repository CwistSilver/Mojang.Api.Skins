using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins.Utilities;
public class ClientOptionsManager
{
    private ClientOptions _options = ClientOptions.Default;
    private readonly object _lock = new object();

    public ClientOptions GetOptions()
    {
        lock (_lock)
        {
            return _options;
        }
    }

    public void SetOptions(ClientOptions options)
    {
        lock (_lock)
        {
            _options = options;
        }
    }
}
