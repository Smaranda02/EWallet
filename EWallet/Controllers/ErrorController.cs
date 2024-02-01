using EWallet.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace EWallet.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(ControllerDependencies dependencies) : base(dependencies) 
        { 
        }

        public IActionResult Index()
        {
            return View("Error");
        }

        public IActionResult NotFound()
        {
            
            return View();

        }

        public IActionResult Unauthorized()
        {
            return View();
        }

        public IActionResult InternalServerError()
        {
            return View();
        }
    }
}
