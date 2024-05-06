namespace GoplayasiaBlazor.Core.Helpers.Interface
{
    public interface IHTTPClientHelper
    {
        Task<T> DeleteAsync<T>(string apiUrl, string authenticationToken);
        Task<T> GetAsync<T>(string apiUrl, string authenticationToken);
        Task<T> PostAsync<T>(string apiUrl, string authenticationToken, object paramsObject);
        Task<T> PutAsync<T>(string apiUrl, string authenticationToken, object paramsObject);
    }
}