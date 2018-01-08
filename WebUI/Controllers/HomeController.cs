using BusnesLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static UserRepository repository;
        private bool isLoadAll = false;
        
        public const int RecordsPerPage = 20;
       
        public List<User> UserData;
        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="_repository"></param>
        public HomeController(UserRepository _repository)
        {
            repository = _repository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("GetUsers");
        }

        /// <summary>
        /// Show PartialView For Create User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowUserCreate()
        {
            return PartialView("_userCreate");
        }

        [HttpPost]
        public ActionResult ShowUserCreate(User user, HttpPostedFileBase uploadImage)
        {
            user.UserImage = ConvertImage(uploadImage);
            user.Signature  = user.Signature ?? "";
            user.Skype = user.Skype ?? "";

            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
            {
                ViewBag.Eror = string.IsNullOrEmpty(user.Name) ? "User Name is Empy" : "User Email is Empy";
                return PartialView("_Eror");
            }
            repository.Add(user);
            return RedirectToAction("GetUsers");
        }

        /// <summary>
        /// Show PartialView For Edit User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult ShowUserEdit(int id)
        {
            if (id == 0)
            {
                ViewBag.Eror = "id == 0 !!! Edit user";
                return PartialView("_Eror");
            }
            var user = repository.GetUser(id);
            ViewBag.User = user;
            return PartialView("_userEdit",user);
        }
        /// <summary>
        /// Edit User and save
        /// </summary>
        /// <param name="user"></param>
        /// <param name="uploadImage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowUserEdit(User user, HttpPostedFileBase uploadImage)
        {
            repository.Update(new User(user.Id,user.Name,user.Email,user.Skype,user.Signature , ConvertImage(uploadImage)));
            return RedirectToAction("GetUsers");
        }

        public ActionResult GetUsers(int? pageNum, string searchStr)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = isLoadAll;
            if (Request.IsAjaxRequest())
            {
                var users = GetRecordsForPage(pageNum.Value, searchStr);
                isLoadAll = (users.Any());
                ViewBag.IsEndOfRecords = isLoadAll;
                return PartialView("_userData", users);
            }
            else
            {
                var projectRep = repository.GetUsers();
                ViewBag.User = GetRecordsForPage(pageNum.Value, searchStr);
                return View("Index");
            }
        }

        /// <summary>
        /// Return List<user> for home/index
        /// </summary>
        /// <param name="pageNum">nomber pages</param>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<User> GetRecordsForPage(int pageNum, string search)
        {
            UserViewModel userView = new UserViewModel();
            userView.SetUser(repository.GetUsers());
            UserData = userView.Users;
            if (!string.IsNullOrEmpty(search))
            {
                pageNum = 0;
                UserData = userView.Users.Where(u => u.Name.Contains(search)).ToList<User>();
            }
            else
            {
                UserData = userView.Users;
            }
            int from = (pageNum * RecordsPerPage);
            var tempList = (from rec in UserData
                            select rec).Skip(from).Take(20).ToList<User>();
            return tempList;
        }

        /// <summary>
        /// Create bytt array for db
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public byte[] ConvertImage(HttpPostedFileBase image)
        {
            byte[] imageData = new byte[] { };
            if (image != null)
            {
                using (var binaryReader = new BinaryReader(image.InputStream))
                {
                    imageData = binaryReader.ReadBytes(image.ContentLength);
                }
            }
            return imageData;
        }
    }
}