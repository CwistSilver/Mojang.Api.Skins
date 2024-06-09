using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;

namespace Mojang.Api.Skins.Utilities;
internal static class HttpClientExtension
{
    internal static void AddSignature(this HttpClient httpClient)
    {
        var version = System.Reflection.Assembly.GetAssembly(typeof(SkinsClient))?.GetName().Version?.ToString() ?? "1.0.0";
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"Minecraft.Api.SkinsClient/{version} (https://github.com/CwistSilver/Mojang.Api.Skins)");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.ExpectContinue = false;
        httpClient.DefaultRequestHeaders.ConnectionClose = false;
    }

    internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(message =>
            {
                if (message.IsSuccessStatusCode)
                    return false;

                return true;
            })
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
