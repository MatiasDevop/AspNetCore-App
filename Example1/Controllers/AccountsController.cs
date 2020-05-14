using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Example1.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly UserManager<UserApplication> processUsers;
        private readonly SignInManager<UserApplication> processLogin;
        public AccountsController(UserManager<UserApplication> processUsers, SignInManager<UserApplication> processLogin)
        {
            this.processUsers = processUsers;
            this.processLogin = processLogin;
        }
        
        [HttpGet]
        [Route("Accounts/Register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Accounts/Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserApplication
                {
                    UserName = model.Email,
                    Email = model.Email,
                    helpPass = model.helpPass
                };

                // Save datos on the table Database AspNetUser
                var response = await processUsers.CreateAsync(user, model.Password);

                //if the user created was correct and log in successfully, redirect to the home Page
                if (response.Succeeded)
                {
                    if (processLogin.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Admin");
                    }
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Accounts/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var response = await processLogin.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberPassword, false);
                if (response.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }  
                }
                ModelState.AddModelError(string.Empty, "Init session not valid");

            }

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        [Route("Accounts/TestEmail")]
        public async Task<IActionResult> TestEmail(string email)
        {
            var user = await processUsers.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"The email {email} it's not available");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Accounts/AccessDeneid")]
        public IActionResult AccessDeneid()
        {
            return View();
        }
    }
}