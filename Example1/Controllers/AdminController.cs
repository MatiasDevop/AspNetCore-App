using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace Example1.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> processRoles;
        private readonly UserManager<UserApplication> processUsers;
        public AdminController(RoleManager<IdentityRole> processRoles,
                                UserManager<UserApplication> processUsers)
        {
            this.processRoles = processRoles;
            this.processUsers = processUsers;
        }
        [HttpGet]
        [Route("Admin/CreateRol")]
        public IActionResult CreateRol()
        {
            return View();
        }
        [HttpPost]
        [Route("Admin/CreateRol")]
        public async Task<IActionResult> CreateRol(CreateRolViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.NameRole
                };
                IdentityResult result = await processRoles.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("Admin/Roles")]
        public IActionResult ListRoles()
        {
            var roles = processRoles.Roles;
            return View(roles);
        }
        [HttpGet]
        [Route("Admin/EditRol")]
        public async Task<IActionResult> EditRol(string id)
        {
            // Search for Rol Id
            var rol = await processRoles.FindByIdAsync(id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol with Id = {id} was not found";
            }

            var model = new EditRolViewModel
            {
                Id = rol.Id,
                NameRol = rol.Name
            };

            //Get all users
            foreach (var user in processUsers.Users)
            {
                if (await processUsers.IsInRoleAsync(user, rol.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }
        [HttpPost]
        [Route("Admin/EditRol")]
        public async Task<IActionResult> EditRol(EditRolViewModel model)
        {
            // Search for Rol Id
            var rol = await processRoles.FindByIdAsync(model.Id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol with Id = {model.Id} was not found";
                return View("Error");
            }
            else
            {
                rol.Name = model.NameRol;
                var result = await processRoles.UpdateAsync(rol);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        [Route("Admin/EditRolUser")]
        public async Task<IActionResult> EditRolUser(string rolId)
        {
            ViewBag.roleId = rolId;

            var role = await processRoles.FindByIdAsync(rolId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol with Id = {rolId} was not found";
                return View("Error");
            }

            var model = new List<UserRolModel>();

            foreach (var user in processUsers.Users)
            {
                var userRolModel = new UserRolModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await processUsers.IsInRoleAsync(user, role.Name))
                {
                    userRolModel.IsSelected = true;
                }
                else
                {
                    userRolModel.IsSelected = false;
                }

                model.Add(userRolModel);
            }

            return View(model);
        }

        [HttpPost]
        [Route("Admin/EditRolUser")]
        public async Task<IActionResult> EditRolUser(List<UserRolModel> model, 
            string rolId)
        {
        
            var role = await processRoles.FindByIdAsync(rolId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol with Id = {rolId} was not found";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await processUsers.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await processUsers.IsInRoleAsync(user, role.Name)))
                {
                    result = await processUsers.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && await processUsers.IsInRoleAsync(user, role.Name))
                {
                    result = await processUsers.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count-1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRol", new { Id = rolId });
                    }
                }
            }

            return RedirectToAction("EditRol", new { Id = rolId });
        }

    }
}