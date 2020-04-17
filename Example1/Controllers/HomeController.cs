using Example1.Models;
using Example1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
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
            //throw new Exception("Forzan error......."); //thits is for development enviroment
            //Friend friend = _friendStore.getFriendData(2);
            DetailsView details = new DetailsView();
            details.Friend = _friendStore.getFriendData(id?? 1);
            details.Title = "Lista Amigos viewModels";
            details.Subtitle = "xxxxxxxxxxxxxxx";
            //ViewData["Header"] = "List friends";
            //ViewData["Friend"] = model;
            if (details.Friend == null)
            {
                Response.StatusCode = 404;
                return View("FriendNotFound", id);
            } 

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
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Friend friend = _friendStore.getFriendData(id);
            EditFriendModel friendEdit = new EditFriendModel
            {
                Id = friend.Id,
                Name = friend.Name,
                Email = friend.Email,
                City = friend.City,
                routePhotoLast = friend.routePhoto
            };
            return View(friendEdit);
        }
        [HttpPost]
        public IActionResult Edit(EditFriendModel model)
        {
            if (ModelState.IsValid)
            {
                //Obtenems the data our Friend from BBDD
                Friend friend = _friendStore.getFriendData(model.Id);
                //Updating the data our object from model
                friend.Name = model.Name;
                friend.Email = model.Email;
                friend.City = model.City;

                if (model.Photo != null)
                {
                    if (model.routePhotoLast != null)
                    {
                        string route = Path.Combine(_hosting.WebRootPath, "images", model.routePhotoLast);
                        System.IO.File.Delete(route);
                    }
                    //SAve the Photo in wwwroot/images
                    friend.routePhoto = UploadImagen(model);
                }

                Friend friendModified = _friendStore.modify(friend);

                return RedirectToAction("index");
            }
            return View(model);
        }
        private string UploadImagen(EditFriendModel model)
        {
            string nameFile = null;
            if (model.Photo != null)
            {
                string folderUpladed = Path.Combine(_hosting.WebRootPath, "images");
                nameFile = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string route = Path.Combine(folderUpladed, nameFile);
                using (var fileStream = new FileStream(route, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return nameFile;
        }

    }
}
