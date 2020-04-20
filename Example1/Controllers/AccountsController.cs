using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Example1.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> processUsers;
        private readonly SignInManager<IdentityUser> processLogin;

        public AccountsController(UserManager<IdentityUser> processUsers, SignInManager<IdentityUser> processLogin)
        {
            this.processUsers = processUsers;
            this.processLogin = processLogin;
        }
        
        [HttpGet]
        [Route("Accounts/Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Accounts/Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                // Save datos on the table Database AspNetUser
                var response = await processUsers.CreateAsync(user, model.Password);

                //if the user created was correct and log in successfully, redirect to the home Page
                if (response.Succeeded)
                {
                    await processLogin.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                // we're gonna controll the errors that goes
                foreach (var error in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("Accounts/Logout")]
        public async Task<IActionResult> LogOut()
        {
            await processLogin.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        [Route("Accounts/Login")]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        [Route("Accounts/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await processLogin.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberPassword, false);
                if (response.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Init session not valid");
            }

            return View(model);
        }
    }
}