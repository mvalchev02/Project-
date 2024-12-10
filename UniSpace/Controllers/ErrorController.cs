using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFoundError()
        {
            return View("404");
        }

        [Route("Error/500")]
        public IActionResult InternalServerError()
        {
            return View("500");
        }
    }

}
