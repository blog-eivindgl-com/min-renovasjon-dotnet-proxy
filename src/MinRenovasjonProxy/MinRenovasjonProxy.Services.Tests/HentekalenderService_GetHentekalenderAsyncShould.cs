using Microsoft.Extensions.Logging;
using MinRenovasjonProxy.Core.Model.NorkartRenovasjon;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace MinRenovasjonProxy.Services.Tests
{
    public class HentekalenderService_GetHentekalenderAsyncShould
    {
        private readonly Mock<ILogger<HentekalenderService>> _loggerMock;
        private readonly Mock<INorkartRenovasjonApiService> _apiServiceMock;
        private readonly HentekalenderService _service;

        public HentekalenderService_GetHentekalenderAsyncShould()
        {
            _loggerMock = new Mock<ILogger<HentekalenderService>>();
            _apiServiceMock = new Mock<INorkartRenovasjonApiService>();
            _service = new HentekalenderService(_loggerMock.Object, _apiServiceMock.Object);
        }

        [Fact]
        public async void GroupAndOrderTommekalenderByDate_AndJoinDescriptionForWasteType()
        {
            // Arrange
            // Setup a tommekalender response with unordered dates
            var fraksjonerJson = TestData.GetResourceAsString(TestData.fraksjoner_response_sample1);
            var fraksjonerResponse = JsonConvert.DeserializeObject<FraksjonerResponse>(fraksjonerJson);
            _apiServiceMock.Setup(m => m.GetFraksjonerAsync()).ReturnsAsync(fraksjonerResponse);
            var tommekalenderJson = TestData.GetResourceAsString(TestData.tommekalender_response_sample1);
            var tommekalenderResponse = JsonConvert.DeserializeObject<TommekalenderResponse>(tommekalenderJson);
            _apiServiceMock.Setup(m => m.GetTommekalenderAsync()).ReturnsAsync(tommekalenderResponse);

            // Act
            var hentekaleder = await _service.GetHentekalenderAsync();

            // Assert
            hentekaleder.ShouldNotBeNull("Some response was returned");
            hentekaleder.Count().ShouldBeGreaterThan(0, "Hentekalender is not empty");
            DateOnly? previousDate = null;

            foreach (var item in hentekaleder)
            {
                if (previousDate != null)
                {
                    previousDate.Value.ShouldBeLessThanOrEqualTo(item.Dato, "Hentekalender is ordered by date");
                    previousDate.Value.ShouldNotBe(item.Dato, "No duplicate dates");
                }

                previousDate = item.Dato;

                item.Avfallstyper.ShouldNotBeNull("Avfallstyper is not null");
                item.Avfallstyper.Count().ShouldBeGreaterThan(0, "No date without avfallstype");

                foreach (var avfallstype in item.Avfallstyper)
                {
                    avfallstype.ShouldNotBeNullOrWhiteSpace("No avfallstype without description");
                }
            }
        }
    }
}
