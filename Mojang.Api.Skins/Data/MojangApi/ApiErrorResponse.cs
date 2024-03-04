using System.Text.Json.Serialization;

namespace Mojang.Api.Skins.Data.MojangApi;

/// <summary>
/// Represents the response structure for errors encountered during API calls.
/// </summary>
public sealed class ApiErrorResponse
{
    /// <summary>
    /// The path of the API request that generated the error.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// The error message associated with the API response.
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; } = string.Empty;
}
