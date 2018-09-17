using LinkLibrary.Entities;
using LinkLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LinkLibrary.Controllers
{
   // [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public AccountController(UserManager<IdentityUser<int>> userManager,
            SignInManager<IdentityUser<int>> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                /* Kuba: Przy wywoływaniu funckji z boolowymi wartościami możesz dodawać nazwy zmiennej, 
                 * bo przy kilku takich boolach można się pogubić co jest czym
                 * np. var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);
                */
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return Redirect("http://localhost:52690/links/"+user.Id);
                    
                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }


        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register()
        {
            LoginViewModel inModel = new LoginViewModel();
            return View(inModel);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        //    [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser<int>() { UserName = loginViewModel.UserName };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    return Redirect("http://localhost:52690/links/" + user.Id);
                }
            }
            return View(loginViewModel);
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
