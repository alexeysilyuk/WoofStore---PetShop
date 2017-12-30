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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();

            ShopItemDAL idal = new ShopItemDAL();
            List<ShopItem> list_of_items = idal.ShopItems.ToList<ShopItem>();


            OrderDAL odal = new OrderDAL();
            List<Order> list_of_orders = odal.Orders.ToList<Order>();

            // if (Convert.ToBoolean(Session["admin"]))

            return View("Index", new AdminViewModel(list_of_users, list_of_items, list_of_orders));


            //else return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public ActionResult EditUser(String  username, String fname, String lname, String phone, String email, String balance, String password
            )
        {

            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();
            User obj = list_of_users.Find(x => x.username == username);



            if (ModelState.IsValid)
            {
                try
                {

                    if (obj != null)
                    {
                        udal.Users.Remove(obj);
                        udal.SaveChanges();

                        obj.fname = fname;
                        obj.lname = lname;
                        obj.phone = phone;
                        obj.email = email;
                        obj.money = Int32.Parse(balance);
                        obj.password = password;

                        udal.Users.Add(obj);
                        udal.SaveChanges();
                    }

                }


                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);

                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }

            return null;
        }



        [HttpPost]
        public ActionResult DeleteUser(String username)
        {

            UserDAL udal = new UserDAL();
            List<User> list_of_users = udal.Users.ToList<User>();
            var obj = list_of_users.Find(x => x.username == username);


            if (obj != null)
            {
                udal.Users.Remove(obj);
                udal.SaveChanges();
            }


            return null;
        }

    }   
}