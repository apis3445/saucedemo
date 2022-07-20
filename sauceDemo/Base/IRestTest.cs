using System.Net.Http;
using System.Threading.Tasks;

namespace sauceDemo.Base;

/// <summary>
/// Added interface to execute api 
/// </summary>
public interface IRestSharpTest
{
    /// <summary>
    /// Add jwt token
    /// </summary>
    /// <param name="token">JWT token</param>
    void AddJwtToken(string token);

    /// <summary>
    /// Method to execute get and return results as json object
    /// </summary>
    /// <typeparam name="T">Object to get results</typeparam>
    /// <param name="urlPath">Url path added to base path</param>
    /// <returns>Object of the T class</returns>
    Task<T> GetAsync<T>(string urlPath) where T : class, new();

    /// <summary>
    /// Method to execute post and return results as json object
    /// </summary>
    /// <typeparam name="T">Object to get results</typeparam>
    /// <param name="urlPath">Url path added to base path</param>
    /// <returns>Object of the T class</returns>
    Task<T> PostJsonAsync<T>(string urlPath, object data) where T : class, new();

    /// <summary>
    /// Method to execute post and return results as HttpResponseMessage
    /// </summary>
    /// <typeparam name="T">Object to get results</typeparam>
    /// <param name="urlPath">Url path added to base path</param>
    /// <returns>HttpResponseMessage</returns>
    Task<HttpResponseMessage> PostAsync<T>(string path, object data) where T : class, new();
}
