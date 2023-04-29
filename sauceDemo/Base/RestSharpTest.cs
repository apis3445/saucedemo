using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using RestSharp;
using RestSharp.Authenticators;

namespace sauceDemo.Base;

/// <summary>
/// Call to rest api service with RestSharp
/// </summary>
public class RestSharpTest : IRestSharpTest
{
    private RestClient _client;
    private RestRequest _request = new RestRequest();

    private string _baseUrl { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="baseUrl">Base Url</param>
    public RestSharpTest(string baseUrl)
    {
        _baseUrl = baseUrl;
        _client = new RestClient(baseUrl);
    }

    /// <summary>
    /// Add jwt token
    /// </summary>
    /// <param name="token"></param>
    public void AddJwtToken(string token)
    {
        _request.AddHeader("Authorization", string.Format("Bearer {0}", token));

    }

    /// <summary>
    /// Execute GET rest api
    /// </summary>
    /// <typeparam name="T">Object to get results</typeparam>
    /// <param name="urlPath">Url path added to base path</param>
    /// <returns>Object of the T class</returns>
    public async Task<T> GetAsync<T>(string urlPath) where T : class, new()
    {
        return await _client.GetJsonAsync<T>(urlPath);
    }

    /// <summary>
    /// Method to execute post and return results as json object
    /// </summary>
    /// <typeparam name="T">Object to get results</typeparam>
    /// <param name="urlPath">Url path added to base path</param>
    /// <returns>Object of the T class</returns>
    public async Task<T> PostJsonAsync<T>(string urlPath, object data) where T : class, new()
    {
        _request.Resource = urlPath;
        _request.Method = Method.Post;
        _request.AddJsonBody(data);
        return await _client.PostAsync<T>(_request);
    }

    /// <summary>
    /// Method to execute post and return results as HttpResponseMessage
    /// </summary>
    /// <typeparam name="T">Object to get results</typeparam>
    /// <param name="urlPath">Url path added to base path</param>
    /// <returns>HttpResponseMessage</returns>
    public async Task<HttpResponseMessage> PostAsync<T>(string path, object data) where T : class, new()
    {
        _request.Resource = path;
        _request.Method = Method.Post;
        _request.AddJsonBody(data);
        var response = await _client.ExecuteAsync(_request);
        var httpResponse = new HttpResponseMessage();
        httpResponse.StatusCode = response.StatusCode;
        httpResponse.ReasonPhrase = response.StatusDescription;
        httpResponse.Version = response.Version;
        httpResponse.Content = new StringContent(response.Content);
        return httpResponse;
    }
}
