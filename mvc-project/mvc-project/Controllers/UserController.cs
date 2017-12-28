using mvc_project.DAL;
using mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_project.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            UserDAL dal = new UserDAL();
            List<User> list_of_users = dal.Users.ToList<User>();

            return View(new UserViewModel(new User(), list_of_users));
        }



        public ActionResult UserSubmit()
        {
            User obj = new Models.User();
            obj.fname = Request.Form["user.fname"];
            obj.lname = Request.Form["user.lname"];
            obj.ID = Int32.Parse(Request.Form["user.ID"]);
            obj.money = Int32.Parse(Request.Form["user.money"]);
            obj.photo = Request.Form["user.photo"];
            obj.email = Request.Form["user.email"];
            obj.phone = Request.Form["user.phone"];
            obj.isAdmin = false;

            if (ModelState.IsValid)
            {
                UserDAL dal = new UserDAL();
                dal.Users.Add(obj);
                dal.SaveChanges();
                List<User> list_of_users = dal.Users.ToList<User>();
                UserViewModel vm = new UserViewModel(new User(), list_of_users);

                return View("Index", vm);
            }
            else
                return View("Index", obj);

        }


    }
}