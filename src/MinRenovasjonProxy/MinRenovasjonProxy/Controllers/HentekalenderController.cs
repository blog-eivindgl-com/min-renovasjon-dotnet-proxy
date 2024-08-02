using Microsoft.AspNetCore.Mvc;
using MinRenovasjonProxy.Core.Model;
using MinRenovasjonProxy.Services;

namespace MinRenovasjonProxy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class HentekalenderController : ControllerBase
    {
        private readonly ILogger<HentekalenderController> _logger;
        private readonly IHentekalenderService _hentekalenderService;

        public HentekalenderController(ILogger<HentekalenderController> logger, IHentekalenderService hentekalenderService)
        {
            _logger = logger;
            _hentekalenderService = hentekalenderService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Hentekalender>> Get()
        {
            _logger.LogDebug("Calling HentekalenderService.GetHentekalenderAsync");

            return await _hentekalenderService.GetHentekalenderAsync();
        }
    }
}
