using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
