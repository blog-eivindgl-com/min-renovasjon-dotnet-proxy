using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MinRenovasjonProxy.Core.Configuration;
using MinRenovasjonProxy.Core.Model.NorkartRenovasjon;
using System.Net.Http.Json;

namespace MinRenovasjonProxy.Services
{
    public class NorkartRenovasjonApiRestService : INorkartRenovasjonApiService
    {
        public const string DefaultBaseUri = @"https://norkartrenovasjon.azurewebsites.net";
        public const string FraksjonerUriPath = @"/proxyserver.ashx?server=https://norkartrenovasjon.azurewebsites.net/proxyserver.ashx?server=https://komteksky.norkart.no/MinRenovasjon.Api/api/fraksjoner";
        public const string GatenavnTemplate = "{gatenavn}";
        public const string GatekodeTemplate = "{gatekode}";
        public const string HusnrTemplate = "{husnr}";
        public const string TommekalenderUriPathTemplate = $"/proxyserver.ashx?server=https://komteksky.norkart.no/MinRenovasjon.Api/api/tommekalender/?gatenavn={GatenavnTemplate}&gatekode={GatekodeTemplate}&husnr={HusnrTemplate}";
        public const string AppKeyRequestHeader = "RenovasjonAppKey";
        public const string KommunenrRequestHeader = "Kommunenr";
        public const string FraksjonerCacheKey = "Fraksjoner";
        public const string TommekalenderCacheKey = "Tommekalender";
        private readonly string _tommekalenderUriPath;
        private readonly ILogger<NorkartRenovasjonApiRestService> _logger;
        private readonly IOptions<NorkartRenovasjonConfiguration> _options;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public NorkartRenovasjonApiRestService(ILogger<NorkartRenovasjonApiRestService> logger, IOptions<NorkartRenovasjonConfiguration> options, HttpClient httpClient, IMemoryCache cache)
        {
            _logger = logger;
            _options = options;
            _cache = cache;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(DefaultBaseUri);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(AppKeyRequestHeader, _options.Value.AppKey);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(KommunenrRequestHeader, _options.Value.Kommunenr);
            _tommekalenderUriPath = TommekalenderUriPathTemplate
                .Replace(GatenavnTemplate, _options.Value.Gatenavn)
                .Replace(GatekodeTemplate, _options.Value.Gatekode)
                .Replace(HusnrTemplate, _options.Value.Husnr);
        }   

        public async Task<FraksjonerResponse?> GetFraksjonerAsync()
        {
            return await _cache.GetOrCreateAsync(
                FraksjonerCacheKey,
                cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24); // Cache values for 24 hours so we don't spam Norkart api
                    return LoadFraksjonerAsync();
                });
        }

        private async Task<FraksjonerResponse?> LoadFraksjonerAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, FraksjonerUriPath);
            _logger.LogDebug($"Requesting Fraksjoner for Kommunenr {_options.Value.Kommunenr}");
            var response = await _httpClient.SendAsync(requestMessage);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                _logger.LogError(await response.Content.ReadAsStringAsync());
                throw;
            }

            try
            {
                var fraksjonerResponse = await response.Content.ReadFromJsonAsync<FraksjonerResponse>();

                if (fraksjonerResponse == null)
                {
                    _logger.LogDebug($"FraksjonerResponse: {await response.Content.ReadAsStringAsync()}");
                }

                return fraksjonerResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing FraksjonerResponse");
                _logger.LogError(await response.Content.ReadAsStringAsync());
                throw;
            }
        }

        public async Task<TommekalenderResponse?> GetTommekalenderAsync()
        {
            return await _cache.GetOrCreateAsync(
                TommekalenderCacheKey,
                cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24); // Cache values for 24 hours so we don't spam Norkart api
                    return LoadTommekalenderAsync();
                });
        }

        private async Task<TommekalenderResponse?> LoadTommekalenderAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _tommekalenderUriPath);
            _logger.LogDebug($"Requesting Tommekalender for Kommunenr {_options.Value.Kommunenr}, Gatekode {_options.Value.Gatekode}, Gatenavn {_options.Value.Gatenavn}, Husnr {_options.Value.Husnr}");
            var response = await _httpClient.SendAsync(requestMessage);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                _logger.LogError(await response.Content.ReadAsStringAsync());
                throw;
            }

            try
            {
                var tommekalenderResponse = await response.Content.ReadFromJsonAsync<TommekalenderResponse>();

                if (tommekalenderResponse == null)
                {
                    _logger.LogDebug($"TommekalenderResponse: {await response.Content.ReadAsStringAsync()}");
                }

                return tommekalenderResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing TommekalenderResponse");
                _logger.LogError(await response.Content.ReadAsStringAsync());
                throw;
            }
        }
    }
}
