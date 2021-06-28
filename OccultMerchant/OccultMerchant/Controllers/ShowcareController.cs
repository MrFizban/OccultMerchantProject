using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OccultMerchant.showcaseitems;

namespace OccultMerchant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowcareController : ControllerBase
    {
        private readonly ILogger<ShowcareController> _logger;

        public ShowcareController(ILogger<ShowcareController> logger)
        {
            _logger = logger;
           
        }

        [HttpGet("/showcase")]
        public List<ShowCase> getActiveShop()
        {
            return ShowCase.GetShowCasesList();
        }
    }
}