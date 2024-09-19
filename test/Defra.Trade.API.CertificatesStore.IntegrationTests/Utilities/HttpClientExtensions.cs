// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.API.CertificatesStore.IntegrationTests.Utilities;

public static class HttpClientExtensions
{
    public static async Task<T?> ReadAsAsync<T>(this HttpContent content)
    {
        var contentStream = await content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<T>(contentStream, GetTradeSerializerOptions());
    }

    public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
        this HttpClient httpClient, string url, T data)
    {
        string dataAsString = JsonSerializer.Serialize(data, GetTradeSerializerOptions());

        var content = new StringContent(dataAsString);

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return httpClient.PostAsync(url, content);
    }

    public static Task<HttpResponseMessage> PutAsJsonAsync<T>(
        this HttpClient httpClient, string url, T data)
    {
        string dataAsString = JsonSerializer.Serialize(data, GetTradeSerializerOptions());

        var content = new StringContent(dataAsString);

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return httpClient.PutAsync(url, content);
    }

    private static JsonSerializerOptions? GetTradeSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        options.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}
