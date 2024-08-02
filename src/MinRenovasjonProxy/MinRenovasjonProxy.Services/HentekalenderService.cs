using Microsoft.Extensions.Logging;
using MinRenovasjonProxy.Core.Model;

namespace MinRenovasjonProxy.Services
{
    public class HentekalenderService : IHentekalenderService
    {
        private readonly ILogger<HentekalenderService> _logger;
        private readonly INorkartRenovasjonApiService _apiService;

        public HentekalenderService(ILogger<HentekalenderService> logger, INorkartRenovasjonApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IEnumerable<Hentekalender>> GetHentekalenderAsync()
        {
            var fraksjoner = await _apiService.GetFraksjonerAsync();
            var tommekalender = await _apiService.GetTommekalenderAsync();
            var avfallstyperPerDate = new Dictionary<DateOnly, List<int>>();

            if (tommekalender == null)
            {
                return new List<Hentekalender>();
            }
            
            // Group type of waste per date
            foreach (var tomming in tommekalender)
            {
                foreach (var date in tomming.Tommedatoer)
                {
                    List<int> avfallsTyper = new List<int>();
                    var dateOnly = DateOnly.FromDateTime(date);

                    if (avfallstyperPerDate.ContainsKey(dateOnly))
                    {
                        avfallsTyper = avfallstyperPerDate[dateOnly];
                    }
                    else
                    {
                        avfallstyperPerDate[dateOnly] = avfallsTyper;
                    }

                    if (!avfallsTyper.Contains(tomming.FraksjonId))
                    {
                        avfallsTyper.Add(tomming.FraksjonId);
                    }
                }
            }

            // Order by date and replace FraksjonId with text for type of waste
            var hentekalender = new List<Hentekalender>();

            foreach (var date in avfallstyperPerDate.Keys)
            {
                var avfallstyper = from f in fraksjoner
                                   join avfallstype in avfallstyperPerDate[date] on f.Id equals avfallstype
                                   select f.Navn;

                hentekalender.Add(new Hentekalender
                {
                    Dato = date,
                    Avfallstyper = avfallstyper
                });
            }

            return hentekalender.OrderBy(x => x.Dato);
        }
    }
}
