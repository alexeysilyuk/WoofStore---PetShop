using mvc_project.DAL;
using mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
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

        // renew al session data if choosen QUIT and redirect to login page
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

        // checking username and password when logging in
        public ActionResult Authorization()
        {

            UserDAL dal = new UserDAL();
            User user = new User();

            string searchCriteria = Request.Form["login"];
            List<User> resultList = (from x in dal.Users
                                     where x.username.Equals(searchCriteria)
                                     select x).ToList<User>();

            //if user with choosen usernam not been found in database, print error message
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

            // encode received password from user, get original password from DB and compare them
            //string receivedPassword = HashPass.GenerateHash(Request.Form["password"]);
            string dbHashPassowrd = user.ecryptedPassword;

            // if not equals, print error in VIEW
            if (!HashPass.ValidatePassword(Request.Form["password"], dbHashPassowrd))
            {
                ViewBag.Error = "User name or password are incorrect!";
                return View("Login", user);
            }
            // if authorization succeed, fill session data
            resultList.ElementAt(0).money += 10;
            dal.SaveChanges();
            Session["login"] = user.username;
            Session["admin"] = user.isAdmin;
            Session["money"] = user.money;
            Session["avatar"] = user.photo;
            //return View("Login", user);
            
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
            User newUser = new Models.User();


            newUser.fname = Request.Form["fname"];
            newUser.username = Request.Form["username"];
            string password = Request.Form["password"];
            newUser.password = "myHappyPassword120!";       // default password, not really used, just used to check REGEX in user model
            newUser.lname = Request.Form["lname"];
            newUser.money = 1000;   // registration bonus
            newUser.photo = Request.Form["photo"];
            newUser.email = Request.Form["email"];
            newUser.phone = Request.Form["phone"];
            // encrypt user password
            newUser.ecryptedPassword = HashPass.GenerateHash(Request.Form["password"]);
            newUser.isAdmin = false;

            // check if user with this username (must be unique) is in DB
            List<User> resultList = (from x in dal.Users
                                     where x.username.Equals(newUser.username)
                                     select x).ToList<User>();
            // if already exists, print error
            if (resultList.Count != 0)
            {
                ViewBag.Error = "User with this username already exists!";
                return View("Login");
            }

            // if not, validate model state and add new user to DB
            if (ModelState.IsValid)
            {
                try
                {
                    dal.Users.Add(newUser);
                    dal.SaveChanges();
                }
                catch (DbEntityValidationException)
                {
                }

                return View("Login");   // redirect to Login page after registration
            }
            else
                return View("Login");

        }


        // show user area
        public ActionResult UserArea()
        {
            if (Session["login"] != null && !(Session["login"].Equals("")))
            {
 
                UserDAL udal = new UserDAL();
                List<User> list_of_users = udal.Users.ToList<User>();
                User obj = list_of_users.Find(x => x.username == Session["login"].ToString());

                return View("UserArea", obj);
            }
            else
                return View("Not_Found");
        }




        // User may update him profile 
        public ActionResult updateProfile(String username, String fname, String lname, String email, String phone)
        {

            // find user inf DB
            UserDAL dal = new UserDAL();
            List<User> list_of_users = dal.Users.ToList<User>();
            User obj = list_of_users.Find(x => x.username == username);

           
            if (obj == null)
            {
                return Json("Error User not exist", JsonRequestBehavior.AllowGet);
            }
            // if found, update profile
            else
            {
                obj.fname = fname;
                obj.lname = lname;
                obj.email = email;
                obj.phone = phone;

                dal.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);

            }
        }

        // update to new password, found user in DB and update password
        public ActionResult updatePassword(String username, String password, String cpassword)
        {
            UserDAL dal = new UserDAL();
            List<User> list_of_users = dal.Users.ToList<User>();
            User obj = list_of_users.Find(x => x.username == username);

            if (obj == null)
            {
                return Json("Error User not exist", JsonRequestBehavior.AllowGet);
            }

            else
            {
                obj.ecryptedPassword = HashPass.GenerateHash(password);

                dal.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);

            }
        }

        // update user avatar
        public ActionResult updatePhoto(String username, String photo_url)
        {
            UserDAL dal = new UserDAL();
            List<User> list_of_users = dal.Users.ToList<User>();
            User obj = list_of_users.Find(x => x.username == username);

            if (obj == null)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            else
            {
                obj.photo = photo_url;
                dal.SaveChanges();

                Session["avatar"] = obj.photo;  // renew avatar in homepage
                return Json(photo_url, JsonRequestBehavior.AllowGet);

            }
        }


    }
}