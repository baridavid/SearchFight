using Application.Common;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Models;
using Application.Models.Bing;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.External
{
    public class BingSearchClient : ISearchClientAsync
    {
        private  readonly HttpClient _httpClient;
        private readonly Settings _settings;
        private readonly string _bingUrl;

        public BingSearchClient(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _bingUrl = _settings.BingURL
                .Replace("{1}", _settings.BingCustomConfig); ;
            _httpClient = new HttpClient
            {
                DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", _settings.BingKey } }
            };
        }

        public string EngineName => Constants.Engines.BING_ENGINE;

        public async Task<long> GetResultsByEngineAsync(string language)
        {

            try
            {
                using (var response = await _httpClient.GetAsync(_bingUrl.Replace("{0}", language)))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new ApiException(SearchFightResource.msg_exception_engine);

                    var result = await response.Content.ReadAsStringAsync();
                    var bingResponse = JsonSerializer.Deserialize<BingResponse>(result);
                    return bingResponse.webPages.totalEstimatedMatches;

                }
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message.ToString());
            }
        }
    }
}
