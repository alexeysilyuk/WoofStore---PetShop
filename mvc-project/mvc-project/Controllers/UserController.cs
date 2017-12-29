using mvc_project.DAL;
using mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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


        public ActionResult LogOut()
        {
            Session["login"] = null;
            Session["admin"] = false;

            Session["money"] = 0;
            Session["avatar"] = null;

            return View("Login");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
        public ActionResult Authorization()
        {

            UserDAL dal = new UserDAL();
            User user = new User();

            string searchCriteria = Request.Form["login"];
            List<User> resultList = (from x in dal.Users
                                     where x.username.Contains(searchCriteria)
                                     select x).ToList<User>();


            if (resultList.Count == 0)
            {

                ViewBag.Error = "User " + searchCriteria + " not found!";
                return View("Login");
            }

            else
            {
                ViewBag.Error = "";
                user = resultList.ElementAt(0);
            }

            string receivedPassword = Request.Form["password"];
            string userPassword = user.password;
            if (!receivedPassword.Equals(userPassword))
            {
                ViewBag.Error = "Incorrect password! Try Again";
                return View("Login", user);
            }

            Session["login"] = user.fname;
            Session["admin"] = user.isAdmin;
            Session["money"] = user.money;
            Session["avatar"] = user.photo;

            return View("~/Views/Home/Index.cshtml");
            //return View("UserArea", user);
        }

        public ActionResult Registration()
        {
            return View("Registration");
        }
        public ActionResult Register()
        {
            UserDAL dal = new UserDAL();
            User obj = new Models.User();


            obj.fname = Request.Form["fname"];
            obj.username = Request.Form["username"];
            string password = Request.Form["password"];
            obj.password = password;
            obj.lname = Request.Form["lname"];
            obj.money = 1000;
            obj.photo = Request.Form["photo"];
            obj.email = Request.Form["email"];
            obj.phone = Request.Form["phone"];
            obj.isAdmin = false;

            List<User> resultList = (from x in dal.Users
                                     where x.username.Contains(obj.username)
                                     select x).ToList<User>();
            if (resultList.Count != 0)
            {
                ViewBag.Error = "USer with this username already exists!";
                return View("Login");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dal.Users.Add(obj);
                    dal.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                }

                // List<User> list_of_users = dal.Users.ToList<User>();
                //UserViewModel vm = new UserViewModel(new User(), list_of_users);

                return View("Login");
            }
            else
                return View("Login");

        }


    }
}