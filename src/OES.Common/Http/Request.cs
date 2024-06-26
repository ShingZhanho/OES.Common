using System.Text;
using Newtonsoft.Json;

namespace OES.Internal;

internal class Request
{
    public object? Body { get; set; }

    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    public IDictionary<string, object>? Parameters { get; set; }

    public string? ContentType
    {
        get => _contentType ?? "application/json";
        set => _contentType = value ?? "application/json";
    }
    private string? _contentType;

    public HttpRequestMessage GetHttpRequestMessage(Uri baseAddress, Uri endpoint)
    {
        var result = new HttpRequestMessage();
        result.Method     = Method;

        if (endpoint.ToString().StartsWith('/')) // removes leading slash(es)
            endpoint = new Uri(endpoint.ToString().TrimStart('/'), UriKind.Relative);
        result.RequestUri = new Uri(baseAddress, endpoint).ApplyParams(Parameters);
        
        foreach (var header in Headers)
            result.Headers.Add(header.Key, header.Value);

        switch (Body)
        {
            case null when Method == HttpMethod.Get || Method == HttpMethod.Post:
                return result; // request body can be null when method is GET or POST
            case null:
                throw new NullReferenceException("Request body is null.");
        }

        result.Content = HttpHelpers.IsBinaryContentType(ContentType!)
            ? new StreamContent(new MemoryStream((byte[])Body))
            : new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8);

        return result;
    }
}