using Microsoft.AspNetCore.Mvc;

namespace MinRenovasjonProxy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class HentekalenderController : ControllerBase
    {
        private readonly ILogger<HentekalenderController> _logger;

        public HentekalenderController(ILogger<HentekalenderController> logger)
        {
            _logger = logger;
        }
    }
}
