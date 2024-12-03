using Microsoft.AspNetCore.Mvc;
using Rehouse.Models;
using System.Diagnostics;

namespace Yungching.Rehouse.Web.Controllers.MVC
{
    public class EstatesController : Controller
    {
        private readonly ILogger<EstatesController> _logger;

        public EstatesController(ILogger<EstatesController> logger)
        {
            _logger = logger;
        }

        //¥¿¦b¾P°â
        public IActionResult Index()
        {
            return View();
        }


        //°±°â
        public IActionResult Discontinue()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
