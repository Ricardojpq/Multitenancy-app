using Authentication.Models.Home;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [AllowAnonymous]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;
        public HomeController(
          IIdentityServerInteractionService interaction,
          IWebHostEnvironment environment,
          ILogger<HomeController> logger)
        {
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Error(string errorId, string errorMessage = null)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from IdentityServer
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }
            else if (!string.IsNullOrEmpty(errorMessage))
            {
                vm.Error = new ErrorMessage();
                vm.Error.Error = errorMessage;
            }

            return View("Error", vm);
        }
    }
}
