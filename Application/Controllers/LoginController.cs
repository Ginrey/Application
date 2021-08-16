using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.DataAccess.Exceptions;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService userService;
        private readonly ILogger<AuthController> logger;

        public AuthController(UserService userService, ILogger<AuthController> logger)
        {
            this.userService = userService;
            this.logger      = logger;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            if (this.User.Identity != null && this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("Index");
            }

            return this.View("Login");
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Registration");
            }

            try
            {
                await this.userService.RegisterUserAsync(request);
            }
            catch (UserAlreadyRegisteredException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                
                return this.View("Registration");
            }
            catch (Exception ex)
            {
                this.ModelState.TryAddModelException(string.Empty, ex);
                
                return this.View("Registration");
            }

            return this.Redirect("Login");
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Login");
            }
            
            try
            {
                var user = await this.userService.AuthUserAsync(request.Login, request.Password);

                await this.Authenticate(user.Login);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View("Login");
            }

            return this.Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return this.RedirectToAction("Index");
        }
        
        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}