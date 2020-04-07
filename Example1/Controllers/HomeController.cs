using Example1.Models;
using Example1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Controllers
{
    public class HomeController : Controller
    {
        private IFriendStore _friendStore;
        private IWebHostEnvironment _hosting;
        public HomeController(IFriendStore FriendStore, IWebHostEnvironment hostingEnvironment)
        {
            _friendStore = FriendStore;
            _hosting = hostingEnvironment;
        }
        //public string Index()
        //{
        //    return _friendStore.getFriendData(1).Email;
        //}
        //public JsonResult Details()
        //{
        //    Friend model = _friendStore.getFriendData(1);
        //    return Json(model);
        //}
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public ViewResult Index(int id)
        {
            var model = _friendStore.GetAllFriends();
            return View(model);
        }
        [Route("Home/Details/{id?}")]
        public ViewResult Details(int? id)
        {
            //Friend friend = _friendStore.getFriendData(2);
            DetailsView details = new DetailsView();
            details.Friend = _friendStore.getFriendData(id?? 1);
            details.Title = "Lista Amigos viewModels";
            details.Subtitle = "xxxxxxxxxxxxxxx";
                //ViewData["Header"] = "List friends";
            //ViewData["Friend"] = model;

            return View(details);
        }
        [Route("Home/Create")]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateFriendModel a)
        {
            if (ModelState.IsValid)
            {
                string guidImagen = null;
                if (a.Photo != null)
                {
                    string ficherosImages = Path.Combine(_hosting.WebRootPath, "images");
                    guidImagen = Guid.NewGuid().ToString() + a.Photo.FileName;
                    string rutaDefenitly = Path.Combine(ficherosImages, guidImagen);
                    a.Photo.CopyTo(new FileStream(rutaDefenitly, FileMode.Create));

                }

                Friend newFriend = new Friend();
                newFriend.Name = a.Name;
                newFriend.Email = a.Email;
                newFriend.City = a.City;
                newFriend.routePhoto = guidImagen;

                _friendStore.nuevo(newFriend);
                return RedirectToAction("details", new { id = newFriend.Id });
            }

            return View();
        }
    }
}
