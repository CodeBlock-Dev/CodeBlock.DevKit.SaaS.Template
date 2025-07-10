using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CanBeYours.Build;

internal static class NuGetPackageHelper
{
    public static async Task<string> GetLatestNuGetVersion(string packageName)
    {
        var url = $"http://api.nuget.org/v3-flatcontainer/{packageName.ToLower()}/index.json";
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to fetch NuGet package versions for {packageName}");

        var jsonContent = await response.Content.ReadAsStringAsync();
        var packageInfo = JsonSerializer.Deserialize<NuGetPackageInfo>(jsonContent);

        if (packageInfo?.Versions == null || !packageInfo.Versions.Any())
            throw new Exception($"No versions found for package {packageName}");

        return packageInfo.Versions.Last();
    }

    private class NuGetPackageInfo
    {
        [JsonPropertyName("versions")]
        public string[] Versions { get; set; }
    }
}
