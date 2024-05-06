using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Helpers.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace GoplayasiaBlazor.Core.Helpers
{
    public class HTTPClientHelper : IHTTPClientHelper
    {
        private readonly HttpClient _client;
        private readonly JsonSerializer _jsonSerializer;
        private readonly IConfiguration _config;
        private readonly string _api;
        public IToastService _toastService;

        public HTTPClientHelper(IConfiguration config, IToastService toastService)
        {
            _toastService = toastService;
            _jsonSerializer = new JsonSerializer();
            _config = config;
            _api = config.GetValue<string>("API");
            if (!_api.EndsWith('/'))
                _api += '/';
            if (_client == null)
            {
                var handler = new HttpClientHandler()
                {
                    //ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    //{
                    //    return true;
                    //},
                    //SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls
                };
                _client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(_api),
                };
            }
        }

        #region METHODS
        public async Task<T> DeleteAsync<T>(string apiUrl, string authenticationToken)
        {
            if (!string.IsNullOrEmpty(authenticationToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            if (apiUrl.StartsWith('/'))
                apiUrl = apiUrl.Remove(0, 1);
            var response = await _client.DeleteAsync(_api + apiUrl);
            if (!response.IsSuccessStatusCode)
                return _jsonSerializer.Deserialize<T>(null);
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            using var json = new JsonTextReader(reader);
            return _jsonSerializer.Deserialize<T>(json);
        }

        public async Task<T> GetAsync<T>(string apiUrl, string authenticationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(authenticationToken))
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
                if (apiUrl.StartsWith('/'))
                    apiUrl = apiUrl.Remove(0, 1);
                var response = await _client.GetAsync(_api + apiUrl);
                if (!response.IsSuccessStatusCode)
                    return _jsonSerializer.Deserialize<T>(null);
                using var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream);
                using var json = new JsonTextReader(reader);
                return _jsonSerializer.Deserialize<T>(json);

            }
            catch (Exception ex)
            {
                throw(ex);
            }
           
        }

        public async Task<T> PostAsync<T>(string apiUrl, string authenticationToken, object paramsObject)
        {
            if (!string.IsNullOrEmpty(authenticationToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            if (apiUrl.StartsWith('/'))
                apiUrl = apiUrl.Remove(0, 1);
            var serializedObject = JsonConvert.SerializeObject(paramsObject);
            var response = await _client.PostAsJsonAsync(_api + apiUrl, paramsObject);
            if (!response.IsSuccessStatusCode)
                return _jsonSerializer.Deserialize<T>(null);
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            using var json = new JsonTextReader(reader);
            return _jsonSerializer.Deserialize<T>(json);
        }

        public async Task<T> PutAsync<T>(string apiUrl, string authenticationToken, object paramsObject)
        {
            if (!string.IsNullOrEmpty(authenticationToken))
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            if (apiUrl.StartsWith('/'))
                apiUrl = apiUrl.Remove(0, 1);
            var response = await _client.PutAsJsonAsync(_api + apiUrl, paramsObject);
            if (!response.IsSuccessStatusCode)
                return _jsonSerializer.Deserialize<T>(null);
            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);
            using var json = new JsonTextReader(reader);
            return _jsonSerializer.Deserialize<T>(json);
        }
        #endregion

      
    }
}
