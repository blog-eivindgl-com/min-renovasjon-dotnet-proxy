using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MinRenovasjonProxy.Core.Configuration;
using MinRenovasjonProxy.Core.Model.NorkartRenovasjon;

namespace MinRenovasjonProxy.Services
{
    public class NorkartRenovasjonApiRestService : INorkartRenovasjonApiService
    {
        private readonly ILogger<NorkartRenovasjonApiRestService> _logger;
        private readonly NorkartRenovasjonConfiguration _config;
        private readonly IMemoryCache _cache;

        public NorkartRenovasjonApiRestService(ILogger<NorkartRenovasjonApiRestService> logger, NorkartRenovasjonConfiguration config, IMemoryCache cache)
        {
            _logger = logger;
            _config = config;
            _cache = cache;
        }   

        public Task<FraksjonerResponse> GetFraksjonerAsync(FraksjonerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<TommekalenderResponse> GetTommekalenderAsync(TommekalenderRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
