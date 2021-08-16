using System;
using System.Threading.Tasks;
using Application.DataAccess.Exceptions;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserService userService;
        private readonly ILogger<RegistrationController> logger;

        public RegistrationController(UserService userService, ILogger<RegistrationController> logger)
        {
            this.userService = userService;
            this.logger      = logger;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Index");
            }

            try
            {
                await this.userService.RegisterUserAsync(request);
            }
            catch (UserAlreadyRegisteredException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                
                return this.View("Index");
            }
            catch (Exception ex)
            {
                this.ModelState.TryAddModelException(string.Empty, ex);
                
                return this.View("Index");
            }

            return this.RedirectToAction("Index", "Login");
        }
    }
}