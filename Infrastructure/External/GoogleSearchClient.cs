using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Models;
using Application.Models.Google;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common;

namespace Infrastructure.External
{
    public class GoogleSearchClient : ISearchClientAsync
    {
        private readonly HttpClient _httpClient;
        private readonly string _googleUrl;
        private readonly Settings _settings;

        public GoogleSearchClient(IOptions<Settings> settings)
        {
            _httpClient = new HttpClient();
            _settings = settings.Value;
            _googleUrl = _settings.GoogleURL
                .Replace("{0}", _settings.GoogleAPIKey)
                .Replace("{1}", _settings.GoogleCEKey);
        }

        public string EngineName => Constants.Engines.GOOGLE_ENGINE;

        public async Task<long> GetResultsByEngineAsync(string language)
        {
            try
            {
                using (var response = await _httpClient.GetAsync(_googleUrl.Replace("{2}", language)))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new ApiException(SearchFightResource.msg_exception_engine);

                    var result = await response.Content.ReadAsStringAsync();
                    var googleResponse = JsonSerializer.Deserialize<GoogleResponse>(result);
                    return long.Parse(googleResponse.searchInformation.totalResults);
                }
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message.ToString());

            }
        }
    }
}
